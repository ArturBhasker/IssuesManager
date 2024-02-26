namespace IssuesManager.DataAccess.Enums
{
    /// <summary>
    /// Статус задачи
    /// </summary>
    public enum IssueStatusEnum
    {
        /// <summary>
        /// Неизвестно
        /// </summary>
        Unknown = 0,
             
        /// <summary>
        /// К выполнению
        /// </summary>
        ToDo = 1,

        /// <summary>
        /// В процессе выполнения
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// Выполнена
        /// </summary>
        Completed = 3,

        /// <summary>
        /// Ошибка при выполнении
        /// </summary>
        Error = 4,
    }
}