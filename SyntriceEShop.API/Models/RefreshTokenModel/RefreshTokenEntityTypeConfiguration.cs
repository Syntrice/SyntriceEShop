using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SyntriceEShop.API.Models.RefreshTokenModel;

public class RefreshTokenEntityTypeConfiguration: IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        // Configure the RefreshToken entity using EF Core fluent api
    }
}