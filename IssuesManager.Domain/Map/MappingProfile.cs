using AutoMapper;
using IssuesManager.Api.Models;
using IssuesManager.DataAccess;
using IssuesManager.Domain.Models;
using IssuesManager.Domain.Repositories;

namespace IssuesManager.Domain.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddOrUpdateIssueDto, IssueEntity>();
            CreateMap<IssueFilterDto, IssueFilter>();
            CreateMap<IssueEntity, IssueDto>();
            CreateMap<IssueFile, IssueFileEntity>().ReverseMap();
            CreateMap<IssueFileEntity, IssueFileDto>();
        }
    }
}
