using System;
using System.Collections.Generic;

namespace AppManager.Domain.Entities;

public class Annex
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public Guid ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public IList<Branch> Branches { get; set; } = [];
}
