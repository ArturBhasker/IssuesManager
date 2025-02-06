using IssuesManager.Contracts.Models.Issues;
using IssuesManager.Domain;
using Microsoft.AspNetCore.Mvc;

namespace IssuesManager.Api.Controllers;

/// <summary>
///     Контролер для работы с заданиями
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly IIssueService _issueService;

    /// <summary>
    ///     ctor
    /// </summary>
    public IssuesController(
        IIssueService issueService)
    {
        _issueService = issueService;
    }

    /// <summary>
    ///     Добавить задание.
    /// </summary>
    /// <param name="addIssue">Модель создания</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add(
        AddIssueDto addIssue,
        CancellationToken cancellationToken)
    {
        var result = await _issueService
            .AddIssueAsync(addIssue, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    ///     Обновить задание.
    /// </summary>
    /// <param name="updateIssue">Модель обновления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> Update(
        UpdateIssueDto updateIssue,
        CancellationToken cancellationToken)
    {
        var result = await _issueService
            .UpdateIssueAsync(updateIssue, cancellationToken);

        return Ok(result);
    }


    /// <summary>
    ///     Получить страницу заданий
    /// </summary>
    /// <param name="filter">Фильтр</param>
    /// <param name="page">Страница</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpGet]
    [Route("getPage")]
    public async Task<ActionResult<IssueDto>> GetPage(
        [FromQuery] IssueFilterDto filter,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await _issueService
            .GetPageAsync(filter, page, pageSize, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    ///     Получить задание по id
    /// </summary>
    /// <param name="id">Id задания</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpGet]
    [Route("get/{id}")]
    public async Task<ActionResult<IssueDto>> Get(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var result = await _issueService
            .GetIssueByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    ///     Удалить задание по id
    /// </summary>
    /// <param name="id">Id записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        await _issueService
            .DeleteIssueAsync(id, cancellationToken);

        return Accepted();
    }
}