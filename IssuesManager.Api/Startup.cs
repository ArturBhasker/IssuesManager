using IssuesManager.Api.Extensions;
using IssuesManager.DataAccess;
using IssuesManager.Domain;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

internal class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers();

        services
            .AddServices()
            .AddMapper();

        services.AddSwaggerGen(c =>
        {
            var xmlName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlName);


            c.IncludeXmlComments(xmlPath);
        });
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