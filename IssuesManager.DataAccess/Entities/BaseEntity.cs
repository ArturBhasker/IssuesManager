namespace IssuesManager.DataAccess.Entities;

/// <summary>
///     Базвоая модель записи
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    ///     Идентификатор записи
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///     Дата создания записи
    /// </summary>
    public DateTimeOffset DateCreated { get; set; }

    /// <summary>
    ///     Дата изменения записи
    /// </summary>
    public DateTimeOffset DateChanged { get; set; }
}