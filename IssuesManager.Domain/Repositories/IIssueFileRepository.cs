using IssuesManager.DataAccess;

namespace IssuesManager.Domain.Repositories
{    /// <summary>
     ///     Репозиторий файлов заданий
     /// </summary>
    public interface IIssueFileRepository
    {
        /// <summary>
        ///     Удалить файлы заданий по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        void DeleteIssueFiles(IssueFileFilter filter);

        /// <summary>
        ///     Получить файл задания по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Файл задания</returns>
        Task<IssueFileEntity> GetIssueFileAsync(
            IssueFileFilter filter,
            CancellationToken cancellationToken
        );
    }
}