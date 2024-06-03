using System;
using AppManager.Domain.Entities;

namespace AppManager.Application.Branches.Queries.GetBranchesWithPagination;

public class BranchBriefDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public bool IsPrivate { get; set; }

    public string Tag { get; set; } = null!;
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Branch, BranchBriefDto>();
        }
    }
}
