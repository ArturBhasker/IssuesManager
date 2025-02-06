using Amazon.S3;
using IssuesManager.Api.Middlewares.Exceptions;
using Microsoft.Extensions.Configuration;

namespace IssuesManager.Domain.Storage;

public class FileStorageService : IFileStorageService
{
    private readonly IAmazonS3 _amazonS3;
    private readonly IConfiguration _configuration;

    public FileStorageService(
        IConfiguration configuration,
        IAmazonS3 amazonS3)
    {
        _amazonS3 = amazonS3;
        _configuration = configuration;

        BucketName = _configuration["AWS:BucketName"];
    }

    private string? BucketName { get; }

    public async Task<string> AddFile(Stream contentStream, CancellationToken cancellationToken)
    {
        var storageId = Guid.NewGuid().ToString();

        await _amazonS3
            .UploadObjectFromStreamAsync(BucketName, storageId, contentStream, default, cancellationToken);

        return storageId;
    }

    public async Task RemoveFilesAsync(string[] storageIds, CancellationToken cancellationToken)
    {
        var deleteTasks = storageIds.Select(i => _amazonS3
            .DeleteObjectAsync(BucketName, i, cancellationToken));

        await Task.WhenAll(deleteTasks);
    }

    public async Task<Stream> ReceiveFileAsync(
        string storageId,
        CancellationToken cancellationToken)
    {
        var contentStream =
            await _amazonS3
                .GetObjectStreamAsync(BucketName, storageId, null, cancellationToken);

        if (contentStream is not null) return contentStream;

        throw new NotFoundException($"Файл с идентификатором {storageId} не найден в хранилище");
    }
}