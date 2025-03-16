using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyntriceEShop.API.Models.ShoppingCartModel;

namespace SyntriceEShop.API.Models.ShoppingCartProductModel;

public class ShoppingCartProductEntityTypeConfiguration : IEntityTypeConfiguration<ShoppingCartProductModel.ShoppingCartProduct>
{
    public void Configure(EntityTypeBuilder<ShoppingCartProductModel.ShoppingCartProduct> builder)
    {
        builder.HasOne<ShoppingCart>(e => e.ShoppingCart).WithMany(e => e.ShoppingCartProducts).HasForeignKey(e => e.ShoppingCartId);
    }
}