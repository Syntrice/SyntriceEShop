using SyntriceEShop.API.Database;
using SyntriceEShop.API.Models.RefreshTokenModel;

namespace SyntriceEShop.API.Repositories;

public class RefreshTokenRepository(ApplicationDbContext db) : IRefreshTokenRepository
{
    public RefreshToken Add(RefreshToken token)
    {
        var entry = db.RefreshTokens.Add(token);
        return entry.Entity;
    }
}