using IssuesManager.Contracts.Models.IssueFiles;

namespace IssuesManager.Contracts.Models.Issues;

public class IssueDto : BaseIssue
{
    /// <summary>
    ///     Идентификатор записи в базе
    /// </summary>
    public required long Id { get; set; }

    /// <summary>
    ///     Файлы задачи
    /// </summary>
    public List<IssueFileDto>? Files { get; set; }
}