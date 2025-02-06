using System.Net;
using IssuesManager.Api.Middlewares.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IssuesManager.Api.Middlewares;

/// <summary>
///     Промежуточный слой для обработки ошибок
/// </summary>
public class ErrorHandlerMiddleware
{
    /// <summary>
    ///     Логгер
    /// </summary>
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    /// <summary>
    ///     Следующий обработчик
    /// </summary>
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    ///     Обработка запроса
    /// </summary>
    /// <param name="context">Контекст HTTP</param>
    /// <param name="correlationIdGenerator">Генератор идентификатора запроса</param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
        }
        catch (InvalidRequestException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
    }

    /// <summary>
    ///     Обработка ошибки и формирование ответа
    /// </summary>
    /// <param name="context">Контекст HTTP</param>
    /// <param name="exception">Исключение</param>
    /// <param name="statusCode">Код статуса</param>
    private Task HandleExceptionAsync(
        HttpContext context,
        Exception exception,
        HttpStatusCode statusCode)
    {
        _logger.LogError(exception, $"Error on processing request {context.Request}");

        var data = exception.Message;

        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(data);
    }
}