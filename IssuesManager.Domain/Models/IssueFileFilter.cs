namespace IssuesManager.Domain.Repositories
{
    /// <summary>
    ///     Фильтр по записям файлов заданий
    /// </summary>
    public class IssueFileFilter
    {
        /// <summary>
        ///     Фильтр по идентификаторам записей
        /// </summary>
        public long[]? Ids { get; set; }

        /// <summary>
        ///     Фильтр по идентификаторам записей в хранилище
        /// </summary>
        public string[]? StorageIds { get; set; }
    }
}