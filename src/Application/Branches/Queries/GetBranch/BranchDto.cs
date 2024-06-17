using System;
using AppManager.Domain.Entities;

namespace AppManager.Application.Branches.Queries.GetBranch;

public class BranchDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public bool IsPrivate { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Branch, BranchDto>();
        }
    }
}
