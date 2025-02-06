using IssuesManager.DataAccess.Entities;
using IssuesManager.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IssuesManager.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:Main"];

        return services
            .AddDbContext<IssuesManagerDbContext>(options => options.UseSqlServer(connectionString))
            .AddScoped<BaseRepository<IssueEntity>>()
            .AddScoped<BaseRepository<IssueFileEntity>>()
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}