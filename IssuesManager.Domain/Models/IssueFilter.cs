using IssuesManager.DataAccess.Enums;

namespace IssuesManager.Domain.Models
{
    /// <summary>
    ///     Фильтр по записям заданий
    /// </summary>
    public class IssueFilter
    {
        /// <summary>
        ///     Фильтр по статусам
        /// </summary>
        public IssueStatusEnum[]? Statuses { get; set; }

        /// <summary>
        ///     Фильтр по идентификаторам записей
        /// </summary>
        public long[]? Ids { get; set; }
    }
}
