using ChoiceWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ChoiceWebApp.Data
{
    public static class DataInitializer
    {
        private const string ADMIN_USERNAME = "admin";
        private const string ADMIN_EMAIL = "admin@admin.com";
        private const string ADMIN_PASSWORD = "adminadmin";

        public static void SetData(UserManager<IdentityUser> userManager, IGroupsJsonIOService groups)
        {
            SetAdmin(userManager);
            SetGroups(groups);
        }

        private static void SetAdmin(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync(ADMIN_USERNAME).Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = ADMIN_USERNAME,
                    Email = ADMIN_EMAIL,
                };

                userManager.CreateAsync(user, ADMIN_PASSWORD).Wait();
            }
        }

        private static void SetGroups(IGroupsJsonIOService groups)
        {
            if (groups.FileIsEmptyOrNotExists())
            {
                groups.WriteAsync(new List<string>()
                    {
                        "ITINF-17-1",
                        "ITINF-18-2",
                        "AKT-18-1",
                        "AKT-19-2",
                        "KIUKI-17-1",
                        "PZPI-18-1"
                    }
                );
            }
        }
    }
}