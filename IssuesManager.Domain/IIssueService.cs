using IssuesManager.Api.Models;
using IssuesManager.DataAccess;
using IssuesManager.Domain.Models;
using IssuesManager.Domain.Repositories;

namespace IssuesManager.Domain
{
    /// <summary>
    ///     Сервис работы с заданиями
    /// </summary>
    public interface IIssueService
    {
        /// <summary>
        ///     Добавить или обновить задание
        /// </summary>
        /// <param name="addOrUpdateIssue">Задание для обновления/добавления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<IssueDto> AddOrUpdateIssueAsync(
            AddOrUpdateIssueDto addOrUpdateIssue,
            CancellationToken cancellationToken);

        /// <summary>
        ///     Получить страницу с заданиями
        /// </summary>
        /// <param name="filter">Задание для обновления/добавления</param>
        /// <param name="page">Страница</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<List<IssueDto>> GetPageAsync
            (
            IssueFilterDto filter,
            int page,
            int pageSize,
            CancellationToken cancellationToken
            );

        /// <summary>
        ///     Получить файла задачи
        /// </summary>
        /// <param name="storageId">Идентификатор файла в хранилище</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<IssueFile?> GetFileAsync(
            string storageId,
            CancellationToken cancellationToken
            );

        /// <summary>
        ///     Удалить задания по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task DeleteAsync(
            IssueFilterDto filter,
            CancellationToken cancellationToken
            );
    }
}