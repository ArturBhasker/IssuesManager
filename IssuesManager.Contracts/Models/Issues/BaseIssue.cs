namespace IssuesManager.Contracts.Models.Issues;

public abstract class BaseIssue
{
    /// <summary>
    ///     Название задания
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Дата задания
    /// </summary>
    public required DateTimeOffset DateOfIssue { get; set; }

    /// <summary>
    ///     Статус задания
    /// </summary>
    public required IssueStatusDtoEnum Status { get; set; }
}