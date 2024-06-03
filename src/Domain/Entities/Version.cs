using System;

namespace AppManager.Domain.Entities;

public class Version
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public string Number { get; set; } = null!;
    
    public Guid BranchId { get; set; }
    
    public Branch Branch { get; set; } = null!;
}
