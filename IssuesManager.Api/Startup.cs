using IssuesManager.Api.Extensions;
using IssuesManager.DataAccess;
using IssuesManager.Domain;
using Swashbuckle.AspNetCore.SwaggerUI;

internal class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers();

        services
            .AddServices()
            .AddMapper();

        services.AddSwaggerGen();
        services.AddDbContext();
    }

    public void Configure(IApplicationBuilder app,
        DataSeedService seedService)
    {
        app
            .UseSwagger()
            .UseSwaggerUI(options => options.DefaultModelRendering(ModelRendering.Model))
            .UseDeveloperExceptionPage()
            .UseRouting()
            .UseEndpoints(e => e.MapControllers())
            ;

        seedService
            .SeedAsync(CancellationToken.None)
            .Wait();
    }
}