using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Db;

public static class SeedIdentityDb
{
    public static async Task InitializeAdminRole(IServiceProvider services)
    {
        var roles = services.GetRequiredService<RoleManager<IdentityRole>>();
        var users = services.GetRequiredService<UserManager<IdentityUser>>();
        
        string[] roleNames = {"Admin","User"};
        
        foreach (var role in roleNames)
            if (!await roles.RoleExistsAsync(role))
                await roles.CreateAsync(new IdentityRole(role));
        
        var adminUser = await users.FindByNameAsync("admin");
        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = "admin@localhost.com",
                Email = "admin@localhost.com",
                EmailConfirmed = true // Подтверждение почты
            };
            var result = await users.CreateAsync(adminUser, "Admin123!@QWzx");
            if (result.Succeeded) await users.AddToRoleAsync(adminUser, "Admin");
        }
        else
        {
            if (!adminUser.EmailConfirmed)
            {
                adminUser.EmailConfirmed = true;
                await users.UpdateAsync(adminUser);
            }

            if (!await users.IsInRoleAsync(adminUser, "Admin"))
                await users.AddToRoleAsync(adminUser, "Admin");
        }
        
        
    }
}