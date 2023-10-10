
using API.Extensions;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructre.Data.Identity;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddDbContext<AppIdentityDbContext>(x=>
                x.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString
            ("DefaultConnectionString")));
        // Add services to the container.
        builder.Services.IdentityServices(builder.Configuration);
        builder.Services.AddScoped<ILoginTimeRepository,LoginTimeRepository>();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        //   app.UseAuthorization();
        //using var scope = app.Services.CreateScope();
        //{
        //    try
        //    {
        //        var userManager = app.Services.GetRequiredService<UserManager<AppUser>>();
        //        var identityContext = app.Services.GetService<ap>

        //    }
        //    catch { 
        //    }
            app.MapControllers();

        app.Run();
    }
}
