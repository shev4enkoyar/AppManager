using System.Collections.Generic;

namespace AppManager.Domain.Entities;

public class Project : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public IList<Annex> Annexes { get; set; } = [];

    public IList<ExternalToken> ExternalTokens { get; set; } = [];
}
