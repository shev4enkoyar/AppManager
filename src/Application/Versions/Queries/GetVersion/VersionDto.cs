using System;
using Version = AppManager.Domain.Entities.Version;

namespace AppManager.Application.Versions.Queries.GetVersion;

public class VersionDto
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public string Number { get; set; } = null!;
    
    public DateTimeOffset CreatedAt { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Version, VersionDto>();
        }
    }
}
