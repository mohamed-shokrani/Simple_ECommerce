

using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructre.Data.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
    {

        if (!userManager.Users.Any()) {

            var user = new AppUser
            {
                UserName = "Shokrani",
                Email = "MohamedShokrani55@gmail.com",
            };
            await userManager.CreateAsync(user,"Pass@@word00");
        }
    } 
}
