using BestAuth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace BestAuth.Infrastructure
{
    public static class SeedData
    {
        private static readonly IReadOnlyCollection<string> DefaultRoles = new[] { "Admin", "User" };

        public static async Task EnsureSeedDataAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<Role>>();
            var userManager = services.GetRequiredService<UserManager<User>>();

            foreach (var roleName in DefaultRoles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new Role
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpperInvariant()
                    };

                    await roleManager.CreateAsync(role);
                }
            }

            await EnsureAdminUserAsync(userManager);
        }

        private static async Task EnsureAdminUserAsync(UserManager<User> userManager)
        {
            const string defaultAdminLogin = "admin";
            const string defaultAdminPassword = "admin";
            const string adminRole = "Admin";

            var adminUser = await userManager.FindByNameAsync(defaultAdminLogin);
            if (adminUser != null)
            {
                if (!await userManager.IsInRoleAsync(adminUser, adminRole))
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }

                return;
            }

            adminUser = User.Create(defaultAdminLogin, defaultAdminLogin, "Administrator");
            adminUser.PasswordHash = userManager.PasswordHasher.HashPassword(adminUser, defaultAdminPassword);

            var createResult = await userManager.CreateAsync(adminUser);
            if (!createResult.Succeeded)
            {
                throw new InvalidOperationException($"Unable to create default admin user: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
            }

            var roleResult = await userManager.AddToRoleAsync(adminUser, adminRole);
            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException($"Unable to assign admin role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }
        }
    }
}
