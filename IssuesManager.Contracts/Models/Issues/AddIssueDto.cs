using Microsoft.AspNetCore.Http;

namespace IssuesManager.Contracts.Models.Issues;

/// <summary>
///     Модель добавления/удаления задания
/// </summary>
public class AddIssueDto : BaseIssue
{
    /// <summary>
    ///     Добавляемые файлы
    /// </summary>
    public IFormFileCollection? Files { get; set; }
}