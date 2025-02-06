using IssuesManager.Contracts.Models.Issues;
using IssuesManager.DataAccess.Entities;

namespace IssuesManager.Domain;

/// <summary>
///     Сервис работы с заданиями
/// </summary>
public interface IIssueService
{
    /// <summary>
    ///     Добавить задание
    /// </summary>
    /// <param name="addIssueDto">Задание для обновления/добавления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<IssueDto> AddIssueAsync(
        AddIssueDto addIssueDto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить задание
    /// </summary>
    /// <param name="updateIssue">Задание для обновления/добавления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<IssueDto> UpdateIssueAsync(
        UpdateIssueDto updateIssue,
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
    ///     Удалить задания по id
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task DeleteIssueAsync(
        long id,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Получить задачу по Id
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<IssueDto> GetIssueByIdAsync(
        long id,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Получить задачу по Id
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<IssueEntity> GetIssueEntityByIdAsync(
        long id,
        CancellationToken cancellationToken);
}