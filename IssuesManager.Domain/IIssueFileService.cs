using IssuesManager.Contracts.Models.IssueFiles;

namespace IssuesManager.Domain;

/// <summary>
///     Сервис работы с файлами заданий
/// </summary>
public interface IIssueFileService
{
    /// <summary>
    ///     Добавить файл задания
    /// </summary>
    /// <param name="fileStream">Стрим файла</param>
    /// <param name="fileName">Наименование файла</param>
    /// <param name="issueId">Идентификатор задания</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<IssueFileDto> AddIssueFileAsync(
        string fileName,
        long issueId,
        Stream fileStream,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить задания по id
    /// </summary>
    /// <param name="storageId">Идентификатор записи в хранилище</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task DeleteIssueFileAsync(
        string storageId,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Получить файл задачи по Id
    /// </summary>
    /// <param name="storageId">Идентификатор файла в хранилище</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<(IssueFileDto fileDto, Stream fileStream)> GetIssueFileByStorageIdAsync(
        string storageId,
        CancellationToken cancellationToken);
}