using AuthenticationOne.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationOne.Interfaces.IRepositories
{
    public interface IAuthReposirory
    {
        Task<AppUser?> FindByEmailAsync(string email);
        Task<IdentityResult?> CreateUserAsync(AppUser user,string password);
        Task<bool>CheckPasswordAsync(AppUser user,string password);
        Task <IdentityResult>AddUserToRoleAsync(AppUser user,string role);
        Task <IdentityResult>RemoveUserToRoleAsync(AppUser user,string role);
        Task<string> GetUserRoleAsync(AppUser user);
    }
}
