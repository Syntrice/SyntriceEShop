using Microsoft.EntityFrameworkCore;
using SyntriceEShop.API.Database;
using SyntriceEShop.API.Models;
using SyntriceEShop.API.Repositories.Interfaces;

namespace SyntriceEShop.API.Repositories.Implementations;

public abstract class GenericRepository<TEntity, TIdType>(ApplicationDbContext db)
    : IGenericRepository<TEntity, TIdType> where TEntity : class, IEntity<TIdType>
{
    public async Task<TEntity?> DeleteByIdAsync(TIdType id)
    {
        var entity = await db.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            Delete(entity);
        }
        return entity;
    }
    
    public TEntity Delete(TEntity entity)
    {
        return db.Set<TEntity>().Remove(entity).Entity;
    }

    public async Task<TEntity?> GetByIdAsync(TIdType id)
    {
        return await db.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await db.Set<TEntity>().ToListAsync();
    }

    public TEntity Add(TEntity entity)
    {
        return db.Set<TEntity>().Add(entity).Entity;
    }

    public TEntity Update(TEntity entity)
    {
        return db.Set<TEntity>().Update(entity).Entity;
    }
}