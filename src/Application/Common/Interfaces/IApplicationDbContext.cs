using System.Threading;
using System.Threading.Tasks;
using AppManager.Domain.Entities;

namespace AppManager.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Project> Projects { get; }
    
    public DbSet<Annex> Annexes { get; }
    
    public DbSet<Branch> Branches { get; }
    
    public DbSet<Tag> Tags { get; }
    
    public DbSet<Version> Versions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
