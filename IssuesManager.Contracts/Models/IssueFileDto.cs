namespace IssuesManager.Api.Models
{
    /// <summary>
    ///     Файл задания
    /// </summary>
    public class IssueFileDto
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор в хранилище
        /// </summary>
        public string StorageId { get; set; }
    }
}
