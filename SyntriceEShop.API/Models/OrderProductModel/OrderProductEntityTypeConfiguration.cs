using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyntriceEShop.API.Models.OrderModel;

namespace SyntriceEShop.API.Models.OrderProductModel;

public class OrderProductEntityTypeConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.HasOne<Order>(e => e.Order).WithMany(e => e.OrderProducts).HasForeignKey(e => e.OrderId);
    }
}