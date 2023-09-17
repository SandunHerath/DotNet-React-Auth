using AuthenticationOne.DTOs;
using AuthenticationOne.Helper.Utils;
using AuthenticationOne.Interfaces.IRepositories;
using AuthenticationOne.Interfaces.IService;
using AuthenticationOne.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace AuthenticationOne.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IAuthReposirory _authReposirory;
        private readonly IConfiguration _configuration;
        public AuthServices(IAuthReposirory authReposirory, IConfiguration configuration)
        {
            _authReposirory = authReposirory;
            _configuration = configuration;
        }

        public async Task<AuthResponce> LoginAsync(LogingRequest logingRequest)
        {
            var userExist = await _authReposirory.FindByEmailAsync(logingRequest.email);
            if (userExist is null)
            {
                throw new Exception(message: "Loging Faild . The Email address isn't commited to an account--" + HttpStatusCode.Unauthorized);
            }
            if(!await _authReposirory.CheckPasswordAsync(userExist, logingRequest.password))
            {
                throw new Exception(message: "Loging Faild . Password is Incorrect --" + HttpStatusCode.BadRequest);
            }
            var token = await GetSecurityTokenAsync(userExist);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResponce
            {
                token = jwt,
                expaied =token.ValidTo
            };
        }

        public async Task<string> RegisterAsync(RegistrationRequest registrationRequest)
        {
            var userExist=await _authReposirory.FindByEmailAsync(registrationRequest.Email);
            if (userExist!=null)
            {
                throw new Exception(message: "user already exist"+HttpStatusCode.BadRequest);
            }
            AppUser user=new AppUser()
            {
                Email = registrationRequest.Email,
                FullName = registrationRequest.FullName,
                UserName = registrationRequest.UserName,
                SecurityStamp=Guid.NewGuid().ToString(),
            };
           var result= await _authReposirory.CreateUserAsync(user, registrationRequest.Password);
            if(result!.Errors.Any())
            {
                StringBuilder stringBuilder=new StringBuilder();
                foreach(var error in result.Errors)
                {
                    stringBuilder.Append(error.Description+"-----");
                }
                throw new Exception(message: "Error : " + stringBuilder);
            }
            if (!result.Succeeded)
            {
                throw new Exception(message: "Registration Faild !");
            }
            await _authReposirory.AddUserToRoleAsync(user, UserRoles.General_User);

            return "Successully Registerd ";
        }

        public async Task<string> AddUserToRoleAsync(ChangeRoleDTO changeRole)
        {
            var userExist = await _authReposirory.FindByEmailAsync(changeRole.Email!);

            if (userExist is null)
            {
                throw new Exception(message: "There Is no user have registerd with this Email" + HttpStatusCode.BadRequest);
            }

            // Get the current roles of the user (if any)
            var currentRole =  await _authReposirory.GetUserRoleAsync(userExist);
            if(currentRole != null)
            {
                // Remove the user from their current roles
                await _authReposirory.RemoveUserToRoleAsync(userExist, currentRole);
            }

            await _authReposirory.AddUserToRoleAsync(userExist, changeRole.RoleTypeEnum.ToString());

            return "Role Added Successully ";
        }

        private async Task<JwtSecurityToken> GetSecurityTokenAsync(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };

            //find the user role and add to the token
            var userRole = await _authReposirory.GetUserRoleAsync(user);
            claims.Add(new Claim(ClaimTypes.Role,userRole));

            var authSigninKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));
            return  new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires:DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["JWT:DurationInMinutes"])),
                claims:claims,
                signingCredentials:new SigningCredentials(authSigninKey,SecurityAlgorithms.HmacSha256)
                );
        }
    }

}
