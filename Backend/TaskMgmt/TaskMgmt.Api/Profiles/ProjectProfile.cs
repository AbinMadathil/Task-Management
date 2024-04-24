using AutoMapper;
using TaskMgmt.Api.DTO.Project;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.Api.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectResponseDto>();
            CreateMap<ProjectDto, Project>();
        }
    }
}
