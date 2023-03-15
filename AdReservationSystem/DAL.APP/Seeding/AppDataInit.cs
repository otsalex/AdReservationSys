using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeding;

public static class AppDataInit
{
    private static Guid adminId = Guid.Parse("adada");
    public static void MigrateDatabase(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }
    public static void DropDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
    }
    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppUser> roleManager)
    {
        (Guid id, string email, string password) userData = (adminId, "admin@app.com", "Hajus1");
        var user = userManager.FindByEmailAsync(userData.email).Result;
        if (user == null)
        {
            user = new AppUser()
            {
                Id = userData.id,
                Email = userData.email,
                UserName = userData.email,
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(user, userData.password).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException("Cannot seed users.");
            }
            
        }
    }
    public static void SeedAppData(ApplicationDbContext context)
    {
       
    }
}

