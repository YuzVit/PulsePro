
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulsePro.Domain.Entities;

namespace PulsePro.Persistence.Configurations;

public class TrainingExerciseConfiguration : BaseEntityConfiguration<TrainingExercise>
{
    public override void Configure(EntityTypeBuilder<TrainingExercise> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.MuscleGroup).IsRequired().HasMaxLength(100);
        builder.Property(e => e.ExerciseName).IsRequired().HasMaxLength(100);
    }
}