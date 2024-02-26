using AutoMapper;
using IssuesManager.Domain;
using IssuesManager.Domain.Map;
using IssuesManager.Domain.Repositories;

namespace IssuesManager.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
               .AddScoped<IIssueRepository, IssueRepository>()
               .AddScoped<IIssueFileRepository, IssueFileRepository>()
               .AddScoped<IUnitOfWork, UnitOfWork>()
               .AddScoped<IFileStorageService, FileStorageService>()
               .AddScoped<IIssueService, IssueService>()
               .AddScoped<DataSeedService>();
        }
        
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(
                mc =>
                {
                    mc.AddProfile<MappingProfile>();

                    mc.AllowNullCollections = true;
                    mc.AllowNullDestinationValues = true;
                });

            var mapper = mappingConfig.CreateMapper();

            return services
                .AddSingleton(mapper);
        }
    }
}
