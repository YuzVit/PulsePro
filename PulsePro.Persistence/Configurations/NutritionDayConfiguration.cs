
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulsePro.Domain.Entities;

namespace PulsePro.Persistence.Configurations;

public class NutritionDayConfiguration : BaseEntityConfiguration<NutritionDay>
{
    public override void Configure(EntityTypeBuilder<NutritionDay> builder)
    {
        base.Configure(builder);

        builder.HasMany(d => d.FoodEntries)
            .WithOne(f => f.NutritionDay)
            .HasForeignKey(f => f.NutritionDayId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}