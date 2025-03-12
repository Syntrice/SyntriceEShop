using SyntriceEShop.API.Database;

namespace SyntriceEShop.API.Repositories;

public class UnitOfWork(ApplicationDbContext db) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await db.SaveChangesAsync(cancellationToken);
    }
}