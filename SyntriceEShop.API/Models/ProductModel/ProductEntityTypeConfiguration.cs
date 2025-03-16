using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyntriceEShop.API.Models.ProductCategoryModel;

namespace SyntriceEShop.API.Models.ProductModel;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne<ProductCategory>(e => e.ProductCategory).WithMany(e => e.Products).HasForeignKey(e => e.ProductCategoryId);
    }
}