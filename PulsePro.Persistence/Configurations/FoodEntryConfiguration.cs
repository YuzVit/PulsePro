
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulsePro.Domain.Entities;

namespace PulsePro.Persistence.Configurations;

public class FoodEntryConfiguration : BaseEntityConfiguration<FoodEntry>
{
    public override void Configure(EntityTypeBuilder<FoodEntry> builder)
    {
        base.Configure(builder);
        builder.Property(f => f.FoodName).IsRequired().HasMaxLength(150);
    }
}