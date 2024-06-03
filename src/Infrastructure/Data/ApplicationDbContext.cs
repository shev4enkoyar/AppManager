using System.Reflection;
using AppManager.Application.Common.Interfaces;
using AppManager.Domain.Entities;
using AppManager.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppManager.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<Project> Projects => Set<Project>();
    
    public DbSet<Annex> Annexes => Set<Annex>();
    
    public DbSet<Branch> Branches => Set<Branch>();
    
    public DbSet<Version> Versions => Set<Version>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
