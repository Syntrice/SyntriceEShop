using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyntriceEShop.API.Models.OrderProductModel;
using SyntriceEShop.API.Models.ProductModel;
using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.API.Models.OrderModel;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne<User>(e => e.User).WithMany(e => e.Orders).HasForeignKey(e => e.UserId);
        builder.HasMany<Product>(e => e.Products).WithMany().UsingEntity<OrderProduct>();
    }
}