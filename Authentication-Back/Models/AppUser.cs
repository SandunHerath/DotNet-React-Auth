using Microsoft.AspNetCore.Identity;

namespace AuthenticationOne.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
