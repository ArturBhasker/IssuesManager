using System.Reflection;
using Amazon.S3;
using IssuesManager.Api.DataSeed;
using IssuesManager.Api.Extensions;
using IssuesManager.Api.Middlewares;
using IssuesManager.DataAccess;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace IssuesManager.Api;

internal class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

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
        services.AddDbContext(_configuration);

        /*        var s3Config = new AmazonS3Config
                {
                    ServiceURL = _configuration.GetSection("AWS:ServiceURL").Value,
                    ForcePathStyle = true
                };*/

        services.AddScoped<IAmazonS3>(a => new AmazonS3Client(_configuration["AWS:User"],
                _configuration["AWS:Password"],
                new AmazonS3Config
                {
                    ServiceURL = _configuration.GetSection("AWS:ServiceURL").Value,
                    ForcePathStyle = true
                }))
            ;
    }

    public void Configure(IApplicationBuilder app,
        DataSeedService seedService,
        IssuesManagerDbContext issuesManagerDbContext,
        IWebHostEnvironment env,
        IAmazonS3 amazonS3Client)
    {
        var bucketName = _configuration["AWS:BucketName"];

        ClearBucketAndDbAsync(amazonS3Client, issuesManagerDbContext, bucketName)
            .GetAwaiter()
            .GetResult();

        amazonS3Client
            .EnsureBucketExistsAsync(bucketName)
            .GetAwaiter()
            .GetResult();

        issuesManagerDbContext
            .Database
            .Migrate();

        if (env.IsDevelopment())
            seedService
                .SeedAsync(CancellationToken.None)
                .Wait();

        app
            .UseMiddleware<ErrorHandlerMiddleware>()
            .UseSwagger()
            .UseSwaggerUI(options => options.DefaultModelRendering(ModelRendering.Model))
            //.UseDeveloperExceptionPage()
            .UseRouting()
            .UseEndpoints(e => e.MapControllers())
            ;
    }

    /// <summary>
    ///     Полностью очисить хранилище и БД
    /// </summary>
    private async Task ClearBucketAndDbAsync(
        IAmazonS3 amazonS3Client,
        IssuesManagerDbContext issuesManagerDbContext,
        string bucketName)
    {
        var bucketsResponse = await amazonS3Client
            .ListBucketsAsync();

        if (bucketsResponse.Buckets.Any(b => b.BucketName == bucketName))
        {
            var objects = await amazonS3Client
                .ListObjectsAsync(bucketName);

            foreach (var obj in objects.S3Objects)
                await amazonS3Client
                    .DeleteObjectAsync(bucketName, obj.Key);

            await amazonS3Client
                .DeleteBucketAsync(bucketName);
        }

        issuesManagerDbContext
            .Database
            .EnsureDeleted();
    }
}