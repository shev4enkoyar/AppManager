using AppManager.Domain.Entities;

namespace AppManager.Application.Projects.Queries.GetProject;

public class ProjectDto
{
    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Project, ProjectDto>();
        }
    }
}
