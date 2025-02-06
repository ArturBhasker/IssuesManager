namespace IssuesManager.DataAccess.Entities;

/// <summary>
///     Файл задания
/// </summary>
public class IssueFileEntity : BaseEntity
{
    /// <summary>
    ///     Имя файла
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Идентификатор в хранилище
    /// </summary>
    public string StorageId { get; set; }

    /// <summary>
    ///     Запись задачи
    /// </summary>
    public IssueEntity Issue { get; set; }
}