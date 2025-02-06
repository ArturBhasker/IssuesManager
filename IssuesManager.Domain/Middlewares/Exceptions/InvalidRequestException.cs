namespace IssuesManager.Api.Middlewares.Exceptions;

/// <summary>
///     Ошибки неправильного запроса
/// </summary>
public class InvalidRequestException : Exception
{
    public InvalidRequestException(string message) : base(message)
    {
    }
}