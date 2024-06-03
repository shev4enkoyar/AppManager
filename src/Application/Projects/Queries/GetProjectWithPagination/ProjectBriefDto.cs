using System;
using AppManager.Domain.Entities;

namespace AppManager.Application.Projects.Queries.GetProjectWithPagination;

public class ProjectBriefDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Project, ProjectBriefDto>();
        }
    }
}
