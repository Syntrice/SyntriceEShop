using SyntriceEShop.API.Models;

namespace SyntriceEShop.API.Repositories.Interfaces;

public interface IGenericRepository<TEntity, in TIdType> where TEntity : IEntity<TIdType>
{
    public Task<TEntity?> DeleteByIdAsync(TIdType id);
    public Task<TEntity?> GetByIdAsync(TIdType id);
    public Task<IEnumerable<TEntity>> GetAllAsync();
    public TEntity Delete(TEntity entity);
    public TEntity Add(TEntity entity);
    public TEntity Update(TEntity entity);
}