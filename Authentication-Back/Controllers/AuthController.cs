using AuthenticationOne.DTOs;
using AuthenticationOne.Helper.Utils;
using AuthenticationOne.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthServices _authServices;
        public AuthController(ILogger<AuthController> logger,IAuthServices authServices) { 
            _logger = logger;
            _authServices = authServices;
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponce>> login(LogingRequest logingRequest)
        {
           var responce= await _authServices.LoginAsync(logingRequest);
            return Ok(responce);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegistrationRequest registrationRequest)
        {
            var responce = await _authServices.RegisterAsync(registrationRequest);
            return Ok(responce);
        }
        [HttpPost("ChangeUserRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> ChangeUserRole(ChangeRoleDTO changeRole)
        {
            var responce= await _authServices.AddUserToRoleAsync(changeRole);
            return Ok(responce);
        }
    }
}
