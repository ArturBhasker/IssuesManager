using AutoMapper;
using IssuesManager.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IssuesManager.Domain.Repositories
{
    public class IssueFileRepository : IIssueFileRepository
    {
        private readonly IssuesManagerDbContext _issuesManagerDbContext;

        public IMapper _mapper { get; }

        public IssueFileRepository(
            IssuesManagerDbContext issuesManagerDbContext,
            IMapper mapper)
        {
            _issuesManagerDbContext = issuesManagerDbContext;
            _mapper = mapper;
        }

        public void DeleteIssueFiles(
            IssueFileFilter filter
            )
        {
            var entitiesToDelete = GetQueryFilter(filter);

            _issuesManagerDbContext
                .RemoveRange(entitiesToDelete);
        }

        public async Task<IssueFileEntity> GetIssueFileAsync(
            IssueFileFilter filter,
            CancellationToken cancellationToken
        )
        {
            var result = await GetQueryFilter(filter)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }

        private IQueryable<IssueFileEntity> GetQueryFilter(IssueFileFilter filter)
        {
            return _issuesManagerDbContext
                .Files
                .Where(entity => 
                    (filter.Ids == null || filter.Ids.Contains(entity.Id))
                    && (filter.StorageIds == null || filter.StorageIds.Contains(entity.StorageId))
                 );
        }
    }
}
