using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SyntriceEShop.API.ApplicationOptions;
using SyntriceEShop.API.Database;
using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.API.Repositories;
using SyntriceEShop.API.Repositories.Implementations;
using SyntriceEShop.API.Repositories.Interfaces;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Implementations;
using SyntriceEShop.API.Services.Interfaces;

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
                    context.Set<User>()
                        .Add(new User() { Id = 1, Username = "user", PasswordHash = "hashedpassword123" });
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
        builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }

    public static void SetupServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
        builder.Services.AddSingleton<IJWTProvider, JWTProvider>();
    }

    public static void SetupAuthentication(this WebApplicationBuilder builder)
    {
        // resolve service for options
        var jwtOptions = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JWTOptions>>();

        builder.Services.AddAuthorization(); // used for policy / role / permissions system
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // configure jwt 
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtOptions.Value.Issuer,
                    ValidateIssuer = true,

                    ValidAudience = jwtOptions.Value.Audience,
                    ValidateAudience = true,

                    ClockSkew = TimeSpan.Zero, // no tolerance for expiration time in the past
                    ValidateLifetime = true,

                    // Create a signing key using the same secret that is used for token generation
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey)),
                    ValidateIssuerSigningKey = true,
                };

                // Implement the abiltiy for the asp.net authentication middleware to get the token from the cookie
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.TryGetValue("accessToken", out var accessTokenFromCookie))
                        {
                            context.Token = accessTokenFromCookie;
                        }
                        // else, try and get the token from the authorization header

                        else if (context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
                        {
                            var tokenValue = authorizationHeader.FirstOrDefault();
                            if (!string.IsNullOrEmpty(tokenValue) && tokenValue.StartsWith("Bearer"))
                            {
                                context.Token = tokenValue.Substring("Bearer ".Length);
                            }
                        }

                        return Task.CompletedTask;
                    }
                };
            });
    }

    public static void SetupSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            // We need to add this so that Swagger knows that the API uses JWT tokens, and provides functionality for 
            // sending tokens
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter your JWT token in this field",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
            };
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    []
                }
            };

            options.AddSecurityRequirement(securityRequirement);
        });
    }
}