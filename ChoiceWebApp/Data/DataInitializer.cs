using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ChoiceWebApp.Data
{
    public static class DataInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager,
                                    RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string user = "User";
            string admin = "Admin";

            if (!roleManager.RoleExistsAsync(user).Result)
            {
                roleManager.CreateAsync(new IdentityRole(user)).Wait();
            }
            if (!roleManager.RoleExistsAsync(admin).Result)
            {
                roleManager.CreateAsync(new IdentityRole(admin)).Wait();
            }
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                };
                var result = userManager.CreateAsync(user, "admin").Result;
                userManager.AddClaimAsync(user, new Claim("FullName", "Admin")).Wait();

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}