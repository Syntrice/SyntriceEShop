using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.API.Models.ShoppingCartModel;

public class ShoppingCartEntityTypeConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.HasIndex(e => e.UserId).IsUnique(); // add a unique index for the user id
        builder.HasOne<User>(e => e.User).WithOne(e => e.ShoppingCart).HasForeignKey<ShoppingCart>(e => e.UserId);
    }
}