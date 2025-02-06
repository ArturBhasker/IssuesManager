using IssuesManager.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace IssuesManager.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IssuesManagerDbContext _issuesManagerDbContext;


    public UnitOfWork(
        BaseRepository<IssueFileEntity> issueFileRepository,
        BaseRepository<IssueEntity> issueRepository,
        IssuesManagerDbContext issuesManagerDbContext)
    {
        IssueFileRepository = issueFileRepository;
        IssueRepository = issueRepository;
        _issuesManagerDbContext = issuesManagerDbContext;
    }

    public BaseRepository<IssueFileEntity> IssueFileRepository { get; }

    public BaseRepository<IssueEntity> IssueRepository { get; }

    public async Task<int> SaveEntitiesAsync(
        CancellationToken cancellationToken)
    {
        var now = DateTimeOffset.UtcNow;

        var addedEntries = _issuesManagerDbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
        var modifiedEntries =
            _issuesManagerDbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);

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