using IssuesManager.DataAccess.Enums;

namespace IssuesManager.DataAccess
{
    /// <summary>
    /// Задание
    /// </summary>
    public class IssueEntity : BaseEntity
    {
        /// <summary>
        /// Дата задания
        /// </summary>
        public DateTimeOffset DateOfIssue { get; set; }

        /// <summary>
        /// Наименование задания
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Статус задания
        /// </summary>
        public IssueStatusEnum Status { get; set; }

        /// <summary>
        /// Файлы задачи
        /// </summary>
        public List<IssueFileEntity> Files { get; set; }
    }
}