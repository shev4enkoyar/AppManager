using System;
using System.Collections.Generic;

namespace AppManager.Domain.Entities;

public class Project
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public IList<Annex> Annexes { get; set; } = [];
}
