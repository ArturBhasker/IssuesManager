using IssuesManager.DataAccess.Entities.Enums;

namespace IssuesManager.DataAccess.Entities;

/// <summary>
///     Задание
/// </summary>
public class IssueEntity : BaseEntity
{
    /// <summary>
    ///     Дата задания
    /// </summary>
    public required DateTimeOffset DateOfIssue { get; set; }

    /// <summary>
    ///     Наименование задания
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Статус задания
    /// </summary>
    public required IssueStatusEnum Status { get; set; }

    /// <summary>
    ///     Файлы задачи
    /// </summary>
    public List<IssueFileEntity>? Files { get; set; }
}