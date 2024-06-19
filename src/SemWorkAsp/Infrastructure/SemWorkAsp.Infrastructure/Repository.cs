using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

/// <inheritdoc cref="IRepository{TEntity}"/>>
public class Repository<TEntity> : IRepository<TEntity> where TEntity: class
{
    protected DbContext DbContext { get; }
    protected DbSet<TEntity> DbSet { get; }

    public Repository(DbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }
    
    public IQueryable<TEntity> GetAll()
    {
        return DbSet.AsNoTracking();
    }

    public IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentException(nameof(predicate));
        }

        return DbSet.Where(predicate).AsNoTracking();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity model, CancellationToken cancellationToken)
    {
        if (model == null)
        {
            throw new ArgumentException(nameof(model));
        }
        var entity = await DbSet.AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }

    public async Task UpdateAsync(TEntity model, CancellationToken cancellationToken)
    {
        if (model == null)
        {
            throw new ArgumentException(nameof(model));
        }
        
        DbSet.Update(model);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        TEntity? entityToDelete = await DbSet.FindAsync(id);
        if (entityToDelete == null)
        {
            throw new ArgumentException(nameof(entityToDelete));
        }
        
        DbSet.Remove(entityToDelete);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}