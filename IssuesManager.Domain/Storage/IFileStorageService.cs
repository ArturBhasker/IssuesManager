namespace IssuesManager.Domain.Storage;

/// <summary>
///     Сервис работы с файловым хранилищем
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    ///     Добавить файл
    /// </summary>
    /// <param name="contentStream">Поток содержимого файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<string> AddFile(Stream contentStream, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить файлы
    /// </summary>
    /// <param name="storageIds">Идентификаторы файла в хранилище</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task RemoveFilesAsync(string[] storageIds, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить файл
    /// </summary>
    /// <param name="storageId">Идентификатор файла в хранилище</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Содержимое файла</returns>
    Task<Stream> ReceiveFileAsync(
        string storageId,
        CancellationToken cancellationToken);
}