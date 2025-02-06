using AutoMapper;
using IssuesManager.Api.Middlewares.Exceptions;
using IssuesManager.Contracts.Models.Issues;
using IssuesManager.DataAccess.Entities;
using IssuesManager.DataAccess.Entities.Enums;
using IssuesManager.DataAccess.Repositories;
using IssuesManager.Domain.Storage;
using Microsoft.EntityFrameworkCore;

namespace IssuesManager.Domain;

public class IssueService : IIssueService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public IssueService(
        IFileStorageService fileStorageService,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _fileStorageService = fileStorageService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IssueDto> AddIssueAsync(
        AddIssueDto addIssue,
        CancellationToken cancellationToken)
    {
        var issueFiles = Array.Empty<IssueFileEntity>();
        var filesToAddForms = addIssue.Files;


        if (filesToAddForms is not null
            && filesToAddForms.Any())
        {
            var issueFileTasks = filesToAddForms
                .Select(async f =>
                {
                    await using var fileStream = f.OpenReadStream();

                    var storageId = await _fileStorageService
                        .AddFile(fileStream, cancellationToken);

                    return new IssueFileEntity
                    {
                        Name = f.FileName,
                        StorageId = storageId
                    };
                });

            issueFiles = await Task.WhenAll(issueFileTasks);
        }

        var issueEntity = _mapper.Map<IssueEntity>(addIssue);
        issueEntity.Files = issueFiles.ToList();

        await _unitOfWork.IssueRepository.AddAsync(issueEntity, cancellationToken);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        return _mapper.Map<IssueDto>(issueEntity);
    }

    public async Task<IssueDto> UpdateIssueAsync(
        UpdateIssueDto updateIssue,
        CancellationToken cancellationToken)
    {
        var updatedEntity = _mapper.Map<IssueEntity>(updateIssue);

        _unitOfWork
            .IssueRepository
            .Update(updatedEntity);

        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        return _mapper.Map<IssueDto>(updatedEntity);
    }

    public async Task<List<IssueDto>> GetPageAsync
    (
        IssueFilterDto filter,
        int page,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        var statuses = _mapper.Map<List<IssueStatusEnum>>(filter.Statuses);

        var issuesDbPage = await _unitOfWork
            .IssueRepository
            .Get(issue => (filter.Ids == null || filter.Ids.Contains(issue.Id))
                          && (filter.Statuses == null || statuses.Contains(issue.Status)))
            .Include(issue => issue.Files)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<IssueDto>>(issuesDbPage);
    }

    public async Task DeleteIssueAsync(
        long id,
        CancellationToken cancellationToken
    )
    {
        var issueEntity = await GetIssueEntityByIdAsync(id, cancellationToken);
        var storageFileList = issueEntity
            .Files
            ?.Select(f => f.StorageId)
            .ToArray();

        if (storageFileList.Any())
            await _fileStorageService.RemoveFilesAsync(storageFileList, cancellationToken);

        _unitOfWork
            .IssueRepository
            .Remove(issueEntity);

        await _unitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }

    public async Task<IssueDto> GetIssueByIdAsync(
        long id,
        CancellationToken cancellationToken)
    {
        var issue = await GetIssueEntityByIdAsync(id, cancellationToken);

        return _mapper.Map<IssueDto>(issue);
    }

    public async Task<IssueEntity> GetIssueEntityByIdAsync(
        long id,
        CancellationToken cancellationToken)
    {
        var issue = await _unitOfWork
            .IssueRepository
            .Get(issue => id == issue.Id)
            .Include(issue => issue.Files)
            .FirstOrDefaultAsync(cancellationToken);

        if (issue is null) throw new NotFoundException($"Запись задачи с id {id} не найдена");

        return issue;
    }
}