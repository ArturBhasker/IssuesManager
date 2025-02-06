using System.Diagnostics;
using IssuesManager.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace IssuesManager.DataAccess;

public class IssuesManagerDbContext : DbContext
{
    public IssuesManagerDbContext()
    {
    }

    public IssuesManagerDbContext(DbContextOptions<IssuesManagerDbContext> options) : base(options)
    {
    }

    /// <summary>
    ///     Задания
    /// </summary>
    public DbSet<IssueEntity> Issues { get; set; }

    /// <summary>
    ///     Файлы
    /// </summary>
    public DbSet<IssueFileEntity> Files { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(l => Debug.WriteLine(l));
    }
}