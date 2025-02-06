using Microsoft.AspNetCore.Mvc;

namespace IssuesManager.Contracts.Models.Issues;

public class IssueFilterDto
{
    [FromQuery] public List<IssueStatusDtoEnum>? Statuses { get; set; }

    [FromQuery] public long[]? Ids { get; set; }
}