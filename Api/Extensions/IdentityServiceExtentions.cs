using Core.Entities.Identity;
using Infrastructre.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class IdentityServiceExtentions
    {
        public static IServiceCollection IdentityServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<AppIdentityDbContext>(x =>
            {
                x.UseSqlServer(config.GetConnectionString("IdentityConnection"));
            });
            services.AddIdentityCore<AppUser>(op => { })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication();
            services.AddAuthorization();
            return services;

            //var builder = services.AddIdentityCore<AppUser>();
            //builder = new IdentityBuilder(builder.UserType, builder.Services);

        }
    }
}
