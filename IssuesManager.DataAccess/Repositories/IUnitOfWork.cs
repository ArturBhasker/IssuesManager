using IssuesManager.DataAccess.Entities;

namespace IssuesManager.DataAccess.Repositories;

/// <summary>
///     unitOfWork базы
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Репозиторий файлов заданий
    /// </summary>
    BaseRepository<IssueFileEntity> IssueFileRepository { get; }

    /// <summary>
    ///     Репозиторий заданий
    /// </summary>
    BaseRepository<IssueEntity> IssueRepository { get; }

    /// <summary>
    ///     Применить транзакцию
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество изменённых записей</returns>
    Task<int> SaveEntitiesAsync(
        CancellationToken cancellationToken);
}