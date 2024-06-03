using AppManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppManager.Infrastructure.Data.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder
            .HasMany(x => x.Versions)
            .WithOne(x => x.Branch)
            .HasForeignKey(x => x.BranchId)
            .HasPrincipalKey(x => x.Id);
    }
}
