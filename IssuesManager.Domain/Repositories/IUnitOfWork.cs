namespace IssuesManager.Domain.Repositories
{
    /// <summary>
    ///     unitOfWork базы
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        ///     Репозиторий файлов заданий
        /// </summary>
        IIssueFileRepository IssueFileRepository { get; }

        /// <summary>
        ///     Репозиторий заданий
        /// </summary>
        IIssueRepository IssueRepository { get; }

        /// <summary>
        ///     Применить транзакцию
        /// </summary>
        Task<int> SaveEntitiesAsync(
           CancellationToken cancellationToken);
    }
}