using System;
using Version = AppManager.Domain.Entities.Version;

namespace AppManager.Application.Versions.Queries.GetVersionsWithPagination;

public class VersionBriefDto
{
    public Guid Id { get; init; }
    
    public string Name { get; init; } = null!;
    
    public string Number { get; init; } = null!;
    
    public DateTimeOffset CreatedAt { get; init; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Version, VersionBriefDto>();
        }
    }
}
