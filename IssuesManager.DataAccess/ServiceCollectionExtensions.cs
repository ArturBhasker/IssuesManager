using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IssuesManager.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            return services
                .AddDbContext<IssuesManagerDbContext>(options => options.UseInMemoryDatabase("Issues"));
        }
    }
}
