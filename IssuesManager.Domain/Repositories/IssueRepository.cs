using AutoMapper;
using IssuesManager.Api.Models;
using IssuesManager.DataAccess;
using IssuesManager.DataAccess.Enums;
using IssuesManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IssuesManager.Domain.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly IssuesManagerDbContext _issuesManagerDbContext;
        private readonly IMapper _mapper;

        public IssueRepository(
            IssuesManagerDbContext issuesManagerDbContext,
            IMapper mapper)
        {
            _issuesManagerDbContext = issuesManagerDbContext;
            _mapper = mapper;
        }

        public async Task<List<IssueEntity>> GetIssuesAsync(
            IssueFilter filter,
            int page,
            int pageSize,
            CancellationToken cancellationToken
            )
        {
            var entities = await GetQueryFilter(filter)
                .Include(e => e.Files)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<IssueEntity>>(entities);
        }

        public async Task<EntityEntry<IssueEntity>> AddOrUpdateIssueAsync(
            AddOrUpdateIssueDto issueDto,
            List<IssueFile> issueFiles,
            CancellationToken cancellationToken
            )
        {
            var issueEntity = _mapper.Map<IssueEntity>(issueDto);
            issueEntity.Files = _mapper.Map<List<IssueFileEntity>>(issueFiles);

            EntityEntry<IssueEntity> entityEntry;

            if (issueEntity.Id == default)
            {
                entityEntry =  await _issuesManagerDbContext
                    .Issues
                    .AddAsync(issueEntity, cancellationToken);
            }
            else
            {
                entityEntry = _issuesManagerDbContext
                    .Issues
                    .Update(issueEntity);
            }

            return entityEntry;
        }

        public void DeleteIssues(
            IssueFilter filter
            )
        {
            var entitiesToDelete = GetQueryFilter(filter);

            _issuesManagerDbContext
                .RemoveRange(entitiesToDelete);
        }

        private IQueryable<IssueEntity> GetQueryFilter(IssueFilter filter)
        {
            var stauses = _mapper.Map<IssueStatusEnum[]?>(filter.Statuses);

            return _issuesManagerDbContext
                .Issues
                .Where(entity =>
                    (stauses == null || !stauses.Any() || stauses.Contains(entity.Status))
                    && (filter.Ids == null || !filter.Ids.Any() || filter.Ids.Contains(entity.Id))
                )
                ;
        }
    }
}
