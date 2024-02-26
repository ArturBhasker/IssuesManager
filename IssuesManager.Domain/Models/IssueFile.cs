namespace IssuesManager.Domain.Repositories
{
    /// <summary>
    /// Файлы задания
    /// </summary>
    public class IssueFile
    {
        /// <summary>
        ///     Идентификатор в базе
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Имя файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Идентификатор в хранилище
        /// </summary>
        public string StorageId { get; set; }
        
        /// <summary>
        ///     Стрим файла
        /// </summary>
        public Stream FileStream { get; set; }
    }
}