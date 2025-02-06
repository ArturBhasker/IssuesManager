using IssuesManager.Contracts.Models.IssueFiles;
using IssuesManager.Domain;
using Microsoft.AspNetCore.Mvc;

namespace IssuesManager.Api.Controllers;

/// <summary>
///     Методы работы с файлами заданий
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class IssueFilesController : ControllerBase
{
    private const string OctetStream = "application/octet-stream";

    private readonly IIssueFileService _issueFileService;

    /// <summary>
    ///     ctor
    /// </summary>
    public IssueFilesController(
        IIssueFileService issueFileService)
    {
        _issueFileService = issueFileService;
    }

    /// <summary>
    ///     Скачать файл по Id из хранилища
    /// </summary>
    /// <param name="storageId">Id из хранилища</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpGet]
    [Route("get/{storageId}")]
    public async Task<IActionResult> DownloadFile(
        string storageId,
        CancellationToken cancellationToken)
    {
        var (fileDb, fileStream) = await _issueFileService
            .GetIssueFileByStorageIdAsync(storageId, cancellationToken);

        return File(fileStream, OctetStream, fileDb.Name);
    }

    /// <summary>
    ///     Удалить файл по Id из хранилища
    /// </summary>
    /// <param name="storageId">Id из хранилища</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete]
    [Route("delete/{storageId}")]
    public async Task<IActionResult> DeleteFile(
        string storageId,
        CancellationToken cancellationToken)
    {
        await _issueFileService
            .DeleteIssueFileAsync(storageId, cancellationToken);

        return Accepted();
    }


    /// <summary>
    ///     Добавить файл в задание
    /// </summary>
    /// <param name="issueId">Идентификатор задачи</param>
    /// <param name="formFile">Файл для загрузки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost]
    [Route("add/{issueId}")]
    public async Task<IssueFileDto> AddFile(
        long issueId,
        IFormFile formFile,
        CancellationToken cancellationToken)
    {
        await using var fileStream = formFile.OpenReadStream();

        return await _issueFileService
            .AddIssueFileAsync(formFile.FileName, issueId, fileStream, cancellationToken);
    }
}