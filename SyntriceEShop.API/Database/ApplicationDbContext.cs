using Microsoft.EntityFrameworkCore;
using SyntriceEShop.API.Models.OrderModel;
using SyntriceEShop.API.Models.OrderProductModel;
using SyntriceEShop.API.Models.ProductCategoryModel;
using SyntriceEShop.API.Models.ProductModel;
using SyntriceEShop.API.Models.RefreshTokenModel;
using SyntriceEShop.API.Models.ShoppingCartModel;
using SyntriceEShop.API.Models.ShoppingCartProductModel;
using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.API.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // configure table models
        new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
        new RefreshTokenEntityTypeConfiguration().Configure(modelBuilder.Entity<RefreshToken>());
        new OrderEntityTypeConfiguration().Configure(modelBuilder.Entity<Order>());
        new ShoppingCartEntityTypeConfiguration().Configure(modelBuilder.Entity<ShoppingCart>());
        new ProductEntityTypeConfiguration().Configure(modelBuilder.Entity<Product>());
        new ProductCategoryEntityTypeConfiguration().Configure(modelBuilder.Entity<ProductCategory>());
        new OrderProductEntityTypeConfiguration().Configure(modelBuilder.Entity<OrderProduct>());
        new ShoppingCartProductEntityTypeConfiguration().Configure(modelBuilder.Entity<ShoppingCartProduct>());
        
        base.OnModelCreating(modelBuilder);
    }
}