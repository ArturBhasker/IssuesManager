namespace IssuesManager.Contracts.Models.Issues;

/// <summary>
///     Обновить задание
/// </summary>
public class UpdateIssueDto : BaseIssue
{
    /// <summary>
    ///     Идентификатор записи в базе
    /// </summary>
    public required long Id { get; set; }
}