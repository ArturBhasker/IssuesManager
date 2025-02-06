using AutoFixture;
using IssuesManager.Contracts.Models.Issues;
using IssuesManager.Domain;

namespace IssuesManager.Api.DataSeed;

public class DataSeedService
{
    private readonly IIssueService _issueService;

    public DataSeedService(
        IIssueService issueService)
    {
        _issueService = issueService;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        var fixture = new Fixture();
        var fileStream =
            File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataSeed", "FileForSeed.txt"));
        var formFile = new FormFile(fileStream, 0, fileStream.Length, "FileForSeed.txt", "FileForSeed.txt");
        var formFileCollection = new FormFileCollection { formFile };

        var model = fixture
            .Build<AddIssueDto>()
            .With(e => e.Files, formFileCollection)
            .Create();

        await _issueService
            .AddIssueAsync(model, cancellationToken);
    }
}