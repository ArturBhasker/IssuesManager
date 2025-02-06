using AutoMapper;
using IssuesManager.Contracts.Models.IssueFiles;
using IssuesManager.Contracts.Models.Issues;
using IssuesManager.DataAccess.Entities;
using IssuesManager.DataAccess.Entities.Enums;

namespace IssuesManager.Domain.Map;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddIssueDto, IssueEntity>()
            .ForMember(
                dest => dest.Files,
                opt => opt.Ignore());
        CreateMap<UpdateIssueDto, IssueEntity>();
        CreateMap<IssueEntity, IssueDto>();
        CreateMap<IssueFileEntity, IssueFileDto>().ReverseMap();
        CreateMap<IssueStatusEnum, IssueStatusDtoEnum>().ReverseMap();
    }
}