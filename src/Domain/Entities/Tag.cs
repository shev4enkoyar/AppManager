using System.Collections.Generic;

namespace AppManager.Domain.Entities;

public class Tag : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public IList<Branch> Branches { get; set; } = [];
}
