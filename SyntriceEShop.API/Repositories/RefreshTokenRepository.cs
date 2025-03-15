using Microsoft.EntityFrameworkCore;
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

    public async Task<RefreshToken?> GetByTokenValue(string token)
    {
        return await db.RefreshTokens.Include(e => e.User).FirstOrDefaultAsync(e => e.Token == token);
    }
    
    public async Task RemoveAllByUserIdAsync(int userId)
    {
        var refreshTokens = await db.RefreshTokens.Where(e => e.UserId == userId).ToListAsync();
        db.RefreshTokens.RemoveRange(refreshTokens);
    }
}