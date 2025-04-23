using PulsePro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PulsePro.Persistence.Data;

namespace PulsePro.Persistence.Seeds;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        // Якщо база вже містить хоча б одного користувача – сидінг не потрібен
        if (await db.Users.AnyAsync()) return;

        var demoUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = "demo",
            Email = "demo@pulse.pro",
            PasswordHash = "demo", // тільки для демонстрації!
            Weight = 80,
            Height = 180,
            BirthDate = new DateTime(1990, 1, 1)
        };

        var plan = new TrainingPlan
        {
            Id = Guid.NewGuid(),
            User = demoUser,
            PlanName = "Full Body Beginner",
            WorkoutsPerWeek = 3,
            SessionLengthMinutes = 60
        };
        plan.Exercises.Add(new TrainingExercise
        {
            Id = Guid.NewGuid(),
            MuscleGroup = "Chest",
            ExerciseName = "Bench Press",
            Sets = 3,
            Reps = 10,
            StartWeight = 40,
            TargetWeight = 60
        });

        var day = new NutritionDay
        {
            Id = Guid.NewGuid(),
            User = demoUser,
            TargetCalories = 2500,
            TargetProtein = 150,
            TargetFat = 70,
            TargetCarbs = 300
        };
        day.FoodEntries.Add(new FoodEntry
        {
            Id = Guid.NewGuid(),
            FoodName = "Oatmeal",
            Calories = 300,
            Protein = 10,
            Fat = 5,
            Carbs = 55,
            Fiber = 8
        });

        var progress = new ProgressRecord
        {
            Id = Guid.NewGuid(),
            User = demoUser,
            Date = DateTime.UtcNow,
            CurrentWeight = 79.5,
            ExercisesPlanned = 3,
            ExercisesCompleted = 3,
            TotalSetsPlanned = 9,
            TotalSetsCompleted = 9,
            PlannedCalories = 2500,
            ActualCalories = 2400
        };

        await db.AddRangeAsync(demoUser, plan, day, progress);
        await db.SaveChangesAsync();
    }
}
