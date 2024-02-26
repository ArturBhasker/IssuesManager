using AutoMapper;
using IssuesManager.Api.Models;
using IssuesManager.DataAccess;
using IssuesManager.Domain.Models;
using IssuesManager.Domain.Repositories;

namespace IssuesManager.Domain
{
    public class IssueService : IIssueService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IssueService(
            IFileStorageService fileStorageService, 
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _fileStorageService = fileStorageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IssueDto> AddOrUpdateIssueAsync(
            AddOrUpdateIssueDto addOrUpdateIssue,
            CancellationToken cancellationToken)
        {
            List<IssueFile>? storageFilesToAdd = null;

            var filesToAddForms = addOrUpdateIssue.FilesToAdd;

            if (filesToAddForms is not null 
                && filesToAddForms.Any())
            {
                storageFilesToAdd = new List<IssueFile>();

                var fileStreams = filesToAddForms
                    .Select(f => f.OpenReadStream());

                foreach(var fileToAdd in filesToAddForms)
                {
                    var fileStream = fileToAdd.OpenReadStream();
                    storageFilesToAdd.Add(new IssueFile
                    {
                        FileStream = fileStream,
                        Name = fileToAdd.FileName,
                        StorageId = Guid.NewGuid().ToString(),
                    });
                }
            }

            var oldEntity = await _unitOfWork
                .IssueRepository
                .GetIssuesAsync(
                    new IssueFilter
                    {
                        Ids = new[] { addOrUpdateIssue.Id ?? default }
                    },
                    1,
                    1,
                    cancellationToken
                );

            var entityEntry = await _unitOfWork
                .IssueRepository
                .AddOrUpdateIssueAsync
                (
                addOrUpdateIssue,
                _mapper.Map<List<IssueFile>>(oldEntity.FirstOrDefault()?.Files ?? new List<IssueFileEntity>())
                    ?.Concat(storageFilesToAdd ?? new List<IssueFile>())
                    .Where(e => !addOrUpdateIssue.FilesToDelete?.Contains(e.StorageId) ?? true)
                    .ToList() ?? new List<IssueFile>(),
                cancellationToken
                );


            if(storageFilesToAdd is not null) 
            {
                _fileStorageService
                    .RemoveFiles(addOrUpdateIssue.FilesToDelete?.ToArray() ?? Array.Empty<string>());

                foreach (var file in storageFilesToAdd)
                {
                    await _fileStorageService
                        .AddFile(file.FileStream, file.StorageId, cancellationToken);
                }
            }

            await _unitOfWork.SaveEntitiesAsync(cancellationToken);

            return _mapper.Map<IssueDto>(entityEntry.Entity);
        }

        public async Task<List<IssueDto>> GetPageAsync
            (
            IssueFilterDto filter, 
            int page,
            int pageSize,
            CancellationToken cancellationToken
            )
        {
            var dbFilter = _mapper.Map<IssueFilter>(filter);

            var dbPage = await _unitOfWork
                .IssueRepository
                .GetIssuesAsync(
                    dbFilter,
                    page,
                    pageSize,
                    cancellationToken
                    );

            return _mapper.Map<List<IssueDto>>(dbPage);
        }

        public async Task DeleteAsync(
            IssueFilterDto filter,
            CancellationToken cancellationToken
            )
        {
            var dbFilter = _mapper.Map<IssueFilter>(filter);

            var page = 1;
            var pageSize = 100;

            List<IssueEntity> issueEntitiesPage;

            do 
            {
                issueEntitiesPage = await _unitOfWork
                    .IssueRepository
                    .GetIssuesAsync
                    (
                    dbFilter,
                    page,
                    pageSize,
                    cancellationToken
                    );

                var storageIds = issueEntitiesPage
                    .SelectMany(i => i.Files)
                    .Select(f => f.StorageId)
                    .ToArray();


                _fileStorageService
                    .RemoveFiles
                    (
                        storageIds
                    );

                page++;
            } while(issueEntitiesPage.Any());

            _unitOfWork
                .IssueRepository
                .DeleteIssues(
                    dbFilter
                    );

            await _unitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }


        public async Task<IssueFile?> GetFileAsync(
            string storageId,
            CancellationToken cancellationToken)
        {
            var dbFilter = new IssueFileFilter()
            {
                StorageIds = new[] { storageId },
            };

            var dbFile = await _unitOfWork
                .IssueFileRepository
                .GetIssueFileAsync(dbFilter, cancellationToken);

            if(dbFile is null) return null;

            var fileStream = _fileStorageService
                .ReceiveFile(storageId);

            if (fileStream is null) return null;

            return new IssueFile
            {
                StorageId = storageId,
                Name = dbFile.Name,
                FileStream = fileStream
            };
        }
    }
}
