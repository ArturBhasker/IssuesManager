using AutoMapper;
using IssuesManager.Api.Middlewares.Exceptions;
using IssuesManager.Contracts.Models.IssueFiles;
using IssuesManager.DataAccess.Entities;
using IssuesManager.DataAccess.Repositories;
using IssuesManager.Domain.Storage;
using Microsoft.EntityFrameworkCore;

namespace IssuesManager.Domain;

public class IssueFileService : IIssueFileService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IIssueService _issueService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;


    public IssueFileService(
        IUnitOfWork unitOfWork,
        IIssueService issueService,
        IFileStorageService fileStorageService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _issueService = issueService;
        _fileStorageService = fileStorageService;
        _mapper = mapper;
    }

    public async Task<IssueFileDto> AddIssueFileAsync(
        string fileName,
        long issueId,
        Stream fileStream,
        CancellationToken cancellationToken)
    {
        var storageId = await _fileStorageService
            .AddFile(fileStream, cancellationToken);

        var issue = await _issueService
            .GetIssueEntityByIdAsync(issueId, cancellationToken);

        await _unitOfWork
            .IssueFileRepository
            .AddAsync(new IssueFileEntity
            {
                Name = fileName,
                StorageId = storageId,
                Issue = issue
            }, cancellationToken);

        await _unitOfWork
            .SaveEntitiesAsync(cancellationToken);

        return new IssueFileDto
        {
            Name = fileName,
            StorageId = storageId
        };
    }

    public async Task DeleteIssueFileAsync(string storageId, CancellationToken cancellationToken)
    {
        await _fileStorageService
            .RemoveFilesAsync([storageId], cancellationToken);

        var issueFileEntityToDelete = await GetIssueFileByStoragePrivateAsync(storageId, cancellationToken);

        _unitOfWork
            .IssueFileRepository
            .Remove(issueFileEntityToDelete);

        await _unitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }

    public async Task<(IssueFileDto fileDto, Stream fileStream)> GetIssueFileByStorageIdAsync(
        string storageId,
        CancellationToken cancellationToken)
    {
        var issueFileEntity = await GetIssueFileByStoragePrivateAsync(storageId, cancellationToken);

        var fileStream = await _fileStorageService
            .ReceiveFileAsync(storageId, cancellationToken);

        return (_mapper.Map<IssueFileDto>(issueFileEntity), fileStream);
    }

    private async Task<IssueFileEntity> GetIssueFileByStoragePrivateAsync(
        string storageId,
        CancellationToken cancellationToken)
    {
        var issueFile = await _unitOfWork
            .IssueFileRepository
            .Get(issueFile => storageId == issueFile.StorageId)
            .FirstOrDefaultAsync(cancellationToken);

        if (issueFile is null) throw new NotFoundException($"Запись файла с storageId {storageId} не найдена");

        return issueFile;
    }
}