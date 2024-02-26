using IssuesManager.Api.Models;
using IssuesManager.DataAccess;
using IssuesManager.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IssuesManager.Domain.Repositories
{
    /// <summary>
    ///     Репозиторий заданий
    /// </summary>
    public interface IIssueRepository
    {
        /// <summary>
        ///     Получить страницу задания
        /// </summary>
        /// <param name="filter">Фильтр по записям заданий</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Страница заданий</returns>
        Task<List<IssueEntity>> GetIssuesAsync(
            IssueFilter filter,
            int page,
            int pageSize,
            CancellationToken cancellationToken
       );

        /// <summary>
        ///     Добавить либо обновить задание
        /// </summary>
        /// <param name="issueDto">Модель задания</param>
        /// <param name="issueFiles">Файлы задания</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Инофрмация о добавленной записи</returns>
        Task<EntityEntry<IssueEntity>> AddOrUpdateIssueAsync(
            AddOrUpdateIssueDto issueDto,
            List<IssueFile> issueFiles,
            CancellationToken cancellationToken
            );

        /// <summary>
        ///     Удалить задания по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        void DeleteIssues
            (
            IssueFilter filter
            );
    }
}