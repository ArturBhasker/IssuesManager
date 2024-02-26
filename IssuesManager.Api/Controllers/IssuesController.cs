using IssuesManager.Api.Models;
using IssuesManager.Domain;
using IssuesManager.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace IssuesManager.Api.Controllers
{
    /// <summary>
    ///     Контролер для работы с заданиями 
    /// </summary>
    [ApiController]
    [Route("api/issues")]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService _issueService;
        private const string OctetStream = "application/octet-stream";
        public IssuesController(
            IIssueService issueService)
        {
            _issueService = issueService;
        }

        /// <summary>
        ///     Добавить/обновить задание.
        ///     Если id не указан, то добавляет новое задание,
        ///     Если id указан то птывается обновить его по заданному id
        /// </summary>
        /// <param name="addOrUpdateIssue">Модель создания/обновления</param>
        [HttpPost]
        [Route("addOrUpdate")]
        public async Task<IActionResult> AddOrUpdate(
            [FromForm] AddOrUpdateIssueDto addOrUpdateIssue,
            CancellationToken cancellationToken)
        {
            var result = await _issueService
                 .AddOrUpdateIssueAsync(addOrUpdateIssue, cancellationToken);

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
        public async Task<IActionResult> GetPage(
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
        ///     Удалить задание по фильтру. Рекомендуется удалять по Id.
        ///     Можно также удалить задания с определённым статусом
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(
            IssueFilterDto filter,
            CancellationToken cancellationToken)
        {
           await _issueService
                .DeleteAsync(filter, cancellationToken);

            return Ok();
        }

        /// <summary>
        ///     Скачать фал по Id из хранилища
        /// </summary>
        /// <param name="storageId">Id из хранилища</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpGet]
        [Route("getFile")]
        public async Task<IActionResult> GetFile(
            string storageId,
            CancellationToken cancellationToken)
        {
            var result = await _issueService
                .GetFileAsync(storageId, cancellationToken);

            if (result is null)
                return NotFound($"Файл с Id {storageId} не найден");

            return File(result.FileStream, OctetStream, result.Name);
        }
    }
}