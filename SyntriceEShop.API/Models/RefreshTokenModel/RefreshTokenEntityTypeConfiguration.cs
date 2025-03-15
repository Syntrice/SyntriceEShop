using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SyntriceEShop.API.Models.RefreshTokenModel;

public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        // Configure the RefreshToken entity using EF Core fluent api
        builder.HasKey(e => e.Id); // has an id (not really needed but here for completeness)
        builder.Property(e => e.Token).HasMaxLength(200); // token max length 200 chars
        builder.HasIndex(e => e.Token).IsUnique(); // add a unique index for the token
        // configure one-to-many relationship with users
        builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);
    }
}