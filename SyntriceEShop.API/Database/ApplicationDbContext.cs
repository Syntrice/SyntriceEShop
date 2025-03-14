using Microsoft.EntityFrameworkCore;
using SyntriceEShop.API.Models.RefreshTokenModel;
using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.API.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
        new RefreshTokenEntityTypeConfiguration().Configure(modelBuilder.Entity<RefreshToken>());
    }
}