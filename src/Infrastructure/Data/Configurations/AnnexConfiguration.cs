using AppManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppManager.Infrastructure.Data.Configurations;

public class AnnexConfiguration : IEntityTypeConfiguration<Annex>
{
    public void Configure(EntityTypeBuilder<Annex> builder)
    {
        builder
            .HasMany(x => x.Branches)
            .WithOne(x => x.Annex)
            .HasForeignKey(x => x.AnnexId)
            .HasPrincipalKey(x => x.Id);
    }
}
