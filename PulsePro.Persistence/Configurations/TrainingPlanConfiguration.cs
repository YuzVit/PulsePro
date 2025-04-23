
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulsePro.Domain.Entities;

namespace PulsePro.Persistence.Configurations;

public class TrainingPlanConfiguration : BaseEntityConfiguration<TrainingPlan>
{
    public override void Configure(EntityTypeBuilder<TrainingPlan> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.PlanName)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasMany(p => p.Exercises)
            .WithOne(e => e.TrainingPlan)
            .HasForeignKey(e => e.TrainingPlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}