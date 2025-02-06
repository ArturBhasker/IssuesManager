using AutoMapper;
using IssuesManager.Api.DataSeed;
using IssuesManager.Domain;
using IssuesManager.Domain.Map;
using IssuesManager.Domain.Storage;

namespace IssuesManager.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IFileStorageService, FileStorageService>()
            .AddScoped<IIssueService, IssueService>()
            .AddScoped<IIssueFileService, IssueFileService>()
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