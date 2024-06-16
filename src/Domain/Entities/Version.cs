using System;

namespace AppManager.Domain.Entities;

public class Version : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public string Number { get; set; } = null!;
    
    public bool IsPublished { get; set; }
    
    public string? FilePath { get; set; }
    
    public Guid BranchId { get; set; }
    
    public Branch Branch { get; set; } = null!;
}
