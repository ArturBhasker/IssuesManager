using IssuesManager.Api.Models;

namespace IssuesManager.DataAccess
{
    /// <summary>
    /// Задание
    /// </summary>
    public class IssueDto
    {
        /// <summary>
        ///     Идентификатор записи в базе
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Название задания
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата задания
        /// </summary>
        public DateTimeOffset DateOfIssue { get; set; }

        /// <summary>
        /// Статус задания
        /// </summary>
        public IssueStatusDtoEnum Status { get; set; }

        /// <summary>
        /// Файлы задачи
        /// </summary>
        public List<IssueFileDto> Files { get; set; }
    }
}