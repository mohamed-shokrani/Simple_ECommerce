
using Core.Entities;
using Core.Interfaces;
using Infrastructre.Data;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {


        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddScoped<ILoginTimeRepository, LoginTimeRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;

    }
}
