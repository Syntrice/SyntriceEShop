namespace SyntriceEShop.API.Repositories;

/// <summary>
/// The IUnitOfWork interface is used to provide a consistent way to save changes to the database, thus allowing
/// encapsulation of saving changes to the database from multiple repositories, rather than having each repository
/// provide their own SaveChangesAsync method.
/// </summary>
public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}