using System.Linq.Expressions;
using IssuesManager.DataAccess.Entities;

namespace IssuesManager.DataAccess.Repositories;

/// <summary>
///     Класс, представляющий собой реализацию паттерна репозиторий,
///     с помощью которого можно взаимодействовать с данными через EF Core
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public class BaseRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly IssuesManagerDbContext _dbCtx;

    public BaseRepository(IssuesManagerDbContext dbCtx)
    {
        _dbCtx = dbCtx;
    }

    /// <summary>
    ///     Метод для получения записей с дополнительным условием, например фильтрацией
    /// </summary>
    /// <param name="selector">Дополнительное условие для выборки</param>
    /// <returns>Коллекция моделей по выборке</returns>
    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> selector)
    {
        return _dbCtx.Set<TEntity>().Where(selector).AsQueryable();
    }

    /// <summary>
    ///     Метод для получения всех записей
    /// </summary>
    /// <returns>Коллекция всех записей</returns>
    public IQueryable<TEntity> GetAll()
    {
        return _dbCtx.Set<TEntity>().AsQueryable();
    }

    /// <summary>
    ///     Добавление новой записи
    /// </summary>
    /// <param name="newEntity">Модель для добавления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task AddAsync(TEntity newEntity, CancellationToken cancellationToken)
    {
        await _dbCtx.Set<TEntity>().AddAsync(newEntity, cancellationToken);
    }

    /// <summary>
    ///     Добавление коллекции объектов
    /// </summary>
    /// <param name="newEntities"></param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task AddRangeAsync(IEnumerable<TEntity> newEntities, CancellationToken cancellationToken)
    {
        await _dbCtx.Set<TEntity>().AddRangeAsync(newEntities, cancellationToken);
    }

    /// <summary>
    ///     Удаление записи
    /// </summary>
    /// <param name="entity">Запись для удаления</param>
    public void Remove(TEntity entity)
    {
        _dbCtx.Set<TEntity>().Remove(entity);
    }

    /// <summary>
    ///     Удаление коллекции записей
    /// </summary>
    /// <param name="entities">Коллекция записей для удаления</param>
    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbCtx.Set<TEntity>().RemoveRange(entities);
    }

    /// <summary>
    ///     Обновить запись
    /// </summary>
    /// <param name="entity">Запись для обновления</param>
    public void Update(TEntity entity)
    {
        _dbCtx.Set<TEntity>().Update(entity);
    }

    /// <summary>
    ///     Обновить коллекцию записей
    /// </summary>
    /// <param name="entities">Коллекция для обновления </param>
    public void UpdateRange(List<TEntity> entities)
    {
        _dbCtx.Set<TEntity>().UpdateRange(entities);
    }
}