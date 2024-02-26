using IssuesManager.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace IssuesManager.Domain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IIssueFileRepository IssueFileRepository { get; }

        public IIssueRepository IssueRepository { get; }

        private IssuesManagerDbContext _issuesManagerDbContext;


        public UnitOfWork(
            IIssueFileRepository issueFileRepository,
            IIssueRepository issueRepository,
            IssuesManagerDbContext issuesManagerDbContext)
        {
            IssueFileRepository = issueFileRepository;
            IssueRepository = issueRepository;
            _issuesManagerDbContext = issuesManagerDbContext;
        }

        public async Task<int> SaveEntitiesAsync(
            CancellationToken cancellationToken)
        {
            var now = DateTimeOffset.UtcNow;

            var addedEntries = _issuesManagerDbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            var modifiedEntries = _issuesManagerDbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);

            foreach (var entry in addedEntries.Where(entry => entry.Entity is BaseEntity))
            {
                var entity = (BaseEntity)entry.Entity;

                entity.DateCreated = now;
                entity.DateChanged = now;
            }

            foreach (var entry in modifiedEntries.Where(entry => entry.Entity is BaseEntity))
            {

                var entity = (BaseEntity)entry.Entity;

                entity.DateChanged = now;
            }

            return await _issuesManagerDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
