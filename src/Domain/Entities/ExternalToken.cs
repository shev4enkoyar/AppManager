using System;
using System.Collections.Generic;
using AppManager.Domain.Enums;

namespace AppManager.Domain.Entities;

public class ExternalToken : BaseAuditableEntity
{
    public string Token { get; set; } = null!;

    public DateTimeOffset ExpireAt { get; set; } = DateTimeOffset.Now.AddDays(30);

    public ExternalTokenType TokenType { get; set; } = ExternalTokenType.Branch;
    
    public IList<Annex> Annexes { get; set; } = [];
    
    public IList<Branch> Branches { get; set; } = [];
    
    public IList<Project> Projects { get; set; } = [];
}
