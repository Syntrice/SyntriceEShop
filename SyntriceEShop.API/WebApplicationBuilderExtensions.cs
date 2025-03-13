using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SyntriceEShop.API.Database;
using SyntriceEShop.API.Repositories;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.UserServices;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API;

public static class WebApplicationBuilderExtensions
{
    public static void SetupDatabase(this WebApplicationBuilder builder)
    {
        // For now this uses Sqlite. In the future, there should be some sort of toggle for 
        // different database / live database if I get around to deploying.

        // check if in development mode
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite("Data Source=SyntriceEShop.db");
                options.UseSeeding((context, _) =>
                {
                    // Seed the development database with test data here
                    context.Set<User>().Add(new User() { Id = 1, Username = "user", PasswordHash = "hashedpassword123" });
                    context.SaveChanges();
                });
            });

            // apply migrations
            using var scope = builder.Services.BuildServiceProvider().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            // context.Database.Migrate();
            // not using migrations for now, so use Ensure deleted and ensure created instead
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        else
        {
            throw new NotImplementedException(
                "Production database system not implemented yet. Please run in development mode " +
                "(set environment variable ASPNETCORE_ENVIRONMENT=Development).");
        }
    }

    public static void SetupRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
    
    public static void SetupServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

    }
}