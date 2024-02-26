namespace IssuesManager.Domain
{
    using AutoFixture;
    using IssuesManager.Api.Models;

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
            var fileStream = File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataSeed", "FileForSeed.txt"));
            var formFile = new FormFile(fileStream, 0, fileStream.Length, "FileForSeed.txt", "FileForSeed.txt");
            var formFileCollection = new FormFileCollection();

            formFileCollection
                .Add(formFile);

            var model = fixture
                .Build<AddOrUpdateIssueDto>()
                .Without(e => e.Id)
                .With(e => e.FilesToAdd, formFileCollection)
                .Without(e => e.FilesToDelete)
                .Create();

            await _issueService
                .AddOrUpdateIssueAsync(model, cancellationToken);
        }
    }
}
