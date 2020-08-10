using Microsoft.AspNetCore.Identity;

namespace ChoiceWebApp.Data
{
    public static class DataInitializer
    {
        private const string ADMIN_USERNAME = "admin";
        private const string ADMIN_EMAIL = "admin@admin.com";
        private const string ADMIN_PASSWORD = "adminadmin";

        public static void SetData(UserManager<IdentityUser> userManager)
        {
            SetAdmin(userManager);
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
    }
}