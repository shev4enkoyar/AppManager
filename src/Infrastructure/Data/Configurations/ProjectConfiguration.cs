using AppManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppManager.Infrastructure.Data.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder
            .HasMany(x => x.Annexes)
            .WithOne(x => x.Project)
            .HasForeignKey(x => x.ProjectId)
            .HasPrincipalKey(x => x.Id);

        builder
            .HasMany(x => x.ExternalTokens)
            .WithMany(x => x.Projects);
    }
}
