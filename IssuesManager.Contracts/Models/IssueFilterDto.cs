using IssuesManager.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace IssuesManager.Domain.Models
{
    public class IssueFilterDto
    {
        [FromQuery]
        public IssueStatusDtoEnum[]? Statuses { get; set; }

        [FromQuery]
        public long[]? Ids { get; set; }
    }
}