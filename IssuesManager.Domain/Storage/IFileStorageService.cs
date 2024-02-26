namespace IssuesManager.Domain
{
    /// <summary>
    ///     Сервис работы с файловым хранилищем
    /// </summary>
    public interface IFileStorageService
    {
        /// <summary>
        ///     Добавить файл
        /// </summary>
        /// <param name="contentStream">Поток содержимого файла</param>
        /// <param name="name">Название файла</param>
        Task AddFile(Stream contentStream, string name, CancellationToken cancellationToken);

        /// <summary>
        ///     Удалить файлы
        /// </summary>
        /// <param name="storageIds">Идентификаторы файла в хранилище</param>
        void RemoveFiles(string[] storageIds);

        /// <summary>
        ///     Получить файл
        /// </summary>
        /// <param name="storageId">Идентификатор файла в хранилище</param>
        /// <returns>Содержимое файла</returns>
        Stream? ReceiveFile(string storageId);
    }
}
