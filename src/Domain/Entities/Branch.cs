using System;
using System.Collections.Generic;

namespace AppManager.Domain.Entities;

public class Branch : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public Guid AnnexId { get; set; }

    public Annex Annex { get; set; } = null!;

    public IList<Version> Versions { get; set; } = [];
    
    public bool IsPrivate { get; set; }
    
    public IList<ExternalToken> ExternalTokens { get; set; } = [];
    
    public IList<Tag> Tags { get; set; } = [];
}
