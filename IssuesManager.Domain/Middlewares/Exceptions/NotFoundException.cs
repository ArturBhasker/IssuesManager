namespace IssuesManager.Api.Middlewares.Exceptions;

/// <summary>
///     Исключение о том, что запись не найдена
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException() : base("NotFound")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }
}