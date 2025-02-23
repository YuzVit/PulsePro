using Microsoft.EntityFrameworkCore;
using PulsePro.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PulsePro.Application.Abstraction
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<TrainingPlan> TrainingPlans { get; }
        DbSet<TrainingExercise> TrainingExercises { get; }
        DbSet<NutritionDay> NutritionDays { get; }
        DbSet<FoodEntry> FoodEntries { get; }
        DbSet<ProgressRecord> ProgressRecords { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}