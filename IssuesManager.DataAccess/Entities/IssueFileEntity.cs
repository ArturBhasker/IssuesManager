namespace IssuesManager.DataAccess
{
    /// <summary>
    ///     Файл задания
    /// </summary>
    public class IssueFileEntity : BaseEntity
    {
        /// <summary>
        ///     Имя файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Идентификатор в хранилище
        /// </summary>
        public string StorageId { get; set; }
    }
}