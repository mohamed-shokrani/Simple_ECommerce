
using Api.Extensions;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructre.Data;
using Infrastructre.Data.Identity;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.IdentityServices(builder.Configuration);
        builder.Services.AppDbContextServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(x => x.AllowAnyHeader()
                         .AllowAnyMethod()
                         .WithOrigins("http://localhost:4200"));

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
