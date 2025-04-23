
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulsePro.Domain.Entities;

namespace PulsePro.Persistence.Configurations;

public class ProgressRecordConfiguration : BaseEntityConfiguration<ProgressRecord>
{
    public override void Configure(EntityTypeBuilder<ProgressRecord> builder)
    {
        base.Configure(builder);
        builder.Property(r => r.Date).IsRequired();
    }
}