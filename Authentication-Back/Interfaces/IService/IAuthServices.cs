using AuthenticationOne.DTOs;
using AuthenticationOne.Models;

namespace AuthenticationOne.Interfaces.IService
{
    public interface IAuthServices
    {
        Task<AuthResponce> LoginAsync(LogingRequest logingRequest);
        Task<string> RegisterAsync(RegistrationRequest registrationRequest);
        Task <string>AddUserToRoleAsync(ChangeRoleDTO changeRole);
    }
}
