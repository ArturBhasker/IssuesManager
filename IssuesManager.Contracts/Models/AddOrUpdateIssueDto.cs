using Microsoft.AspNetCore.Http;

namespace IssuesManager.Api.Models
{
    /// <summary>
    /// Модель добавления/удаления задания
    /// </summary>
    public class AddOrUpdateIssueDto
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
        /// Добавялемые файлы
        /// </summary>
        public IFormFileCollection? FilesToAdd { get; set; }

        /// <summary>
        /// Удаляемые файлы
        /// </summary>
        public List<string>? FilesToDelete { get; set; }
    }
}
