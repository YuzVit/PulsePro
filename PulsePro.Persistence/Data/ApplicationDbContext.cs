using Microsoft.EntityFrameworkCore;
using PulsePro.Application.Abstraction;
using PulsePro.Domain.Entities;
using PulsePro.Persistence.Configurations;

namespace PulsePro.Persistence.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    
    public DbSet<User>             Users             => Set<User>();
    public DbSet<TrainingPlan>     TrainingPlans     => Set<TrainingPlan>();
    public DbSet<TrainingExercise> TrainingExercises => Set<TrainingExercise>();
    public DbSet<NutritionDay>     NutritionDays     => Set<NutritionDay>();
    public DbSet<FoodEntry>        FoodEntries       => Set<FoodEntry>();
    public DbSet<ProgressRecord>   ProgressRecords   => Set<ProgressRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // реєструємо всі конфігурації
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TrainingPlanConfiguration());
        modelBuilder.ApplyConfiguration(new TrainingExerciseConfiguration());
        modelBuilder.ApplyConfiguration(new NutritionDayConfiguration());
        modelBuilder.ApplyConfiguration(new FoodEntryConfiguration());
        modelBuilder.ApplyConfiguration(new ProgressRecordConfiguration());
    }
}