namespace IssuesManager.Contracts.Models.IssueFiles;

/// <summary>
///     Файл задания
/// </summary>
public class IssueFileDto : BaseFile
{
    /// <summary>
    ///     Имя файла
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Идентификатор файла в хранилище
    /// </summary>
    public string StorageId { get; set; }
}