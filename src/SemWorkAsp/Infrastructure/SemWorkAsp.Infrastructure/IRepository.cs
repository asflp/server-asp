using System.Linq.Expressions;

namespace Infrastructure;

/// <summary>
/// Базовый репозиторий для работы сущностями
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public interface IRepository<TEntity> where TEntity: class
{
    /// <summary>
    /// Возвращает все элементы сущности
    /// </summary>
    /// <returns>Все элементы сущности</returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Возвращает все элементы сущности по предикату
    /// </summary>
    /// <param name="predicate">Предикат для фильтрации</param>
    /// <returns>Все элементы сущности, подходящие по фильтру</returns>
    IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<TEntity> AddAsync(TEntity model, CancellationToken cancellationToken);

    Task UpdateAsync(TEntity model, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}