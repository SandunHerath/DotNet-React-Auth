using AuthenticationOne.Helper.Utils;
using AuthenticationOne.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationOne.Helper.Static
{
    public static class UserRoleInitializer
    {
        public static async Task RoleInitialize(IServiceScope serviceScope, IConfiguration configuration)
        {

            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.Company_Owner))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Company_Owner));
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.General_User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.General_User));
            }

            //add a Admin initialy
            var userManager=serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            if(await userManager.FindByEmailAsync(configuration["AdminCredentials:Email"]!) is null) {
                var admin = new AppUser
                {
                    Email = configuration["AdminCredentials:Email"],
                    FullName = configuration["AdminCredentials:FullName"],
                    PhoneNumber = configuration["AdminCredentials:PhoneNumber"],
                    UserName= configuration["AdminCredentials:UserName"],
                    SecurityStamp=Guid.NewGuid().ToString(),
                };
                await userManager.CreateAsync(admin, configuration["AdminCredentials:Password"]!);
                await userManager.AddToRoleAsync(admin,UserRoles.Admin);
            }
        }
    }
}
