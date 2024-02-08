using Microsoft.AspNetCore.Identity;

namespace PechinchaMarket.Areas.Identity.Data
{
    public static class DataSeeder
    {
        public static async Task SeedData(UserManager<PechinchaMarketUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedAdminUser(userManager);
        }
        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Comerciante", "Cliente", "Manager" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
        private static async Task SeedAdminUser(UserManager<PechinchaMarketUser> userManager)
        {
            if (await userManager.FindByEmailAsync("admin@gmail.com") == null)
            {
                var user = new PechinchaMarketUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(user, "Senha123.");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Manager");
                }
            }
        }

    }
}
