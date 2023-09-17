using AuthenticationOne.Helper.Utils;
using AuthenticationOne.Interfaces.IRepositories;
using AuthenticationOne.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationOne.Repositories
{
    public class AuthRepository : IAuthReposirory
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddUserToRoleAsync(AppUser user, string role)
        {
           return await _userManager.AddToRoleAsync(user,role);
        }

        public async Task<IdentityResult?> CreateUserAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<AppUser?> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<string> GetUserRoleAsync(AppUser user)
        {
            return (await _userManager.GetRolesAsync(user)).FirstOrDefault()!;
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
           return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> RemoveUserToRoleAsync(AppUser user, string role)
        {
            // Create a collection with a single role
            var rolesToRemove = new List<string> { role };

            return await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

        }
    }
}
