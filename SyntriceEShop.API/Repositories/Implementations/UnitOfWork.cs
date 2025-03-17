using SyntriceEShop.API.Database;
using SyntriceEShop.API.Repositories.Interfaces;

namespace SyntriceEShop.API.Repositories.Implementations;

/// <summary>
/// The UnitOfWork class is used to provide a consistent way to save changes to the database, thus allowing
/// encapsulation of saving changes to the database from multiple repositories, rather than having each repository
/// provide their own SaveChangesAsync method.
/// </summary>
public class UnitOfWork(ApplicationDbContext db) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await db.SaveChangesAsync(cancellationToken);
    }
}