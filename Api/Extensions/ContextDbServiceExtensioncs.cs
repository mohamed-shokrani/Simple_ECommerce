using Core.Entities.Identity;
using Infrastructre.Data.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Extensions;

public static class ContextDbServiceExtensioncs
{

    public static IServiceCollection AppDbContextServices(this IServiceCollection services, IConfiguration config)
    {

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
            config.GetConnectionString
            ("DefaultConnectionString")));
        services.AddDbContext<AppIdentityDbContext>(x =>
        {
            x.UseSqlServer(config.GetConnectionString("IdentityConnection"));
        });

        return services;
    }
    public static IServiceCollection IdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppIdentityDbContext>(x =>
        {
            x.UseSqlServer(config.GetConnectionString("IdentityConnection"));
        });
        services.AddIdentityCore<AppUser>(op => { })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                         config["Token:Key"])),
                    ValidIssuer = config["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        services.AddAuthorization();
        return services;

        //var builder = services.AddIdentityCore<AppUser>();
        //builder = new IdentityBuilder(builder.UserType, builder.Services);

    }
}
