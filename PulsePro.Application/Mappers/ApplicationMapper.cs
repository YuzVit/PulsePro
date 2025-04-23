using System;
using System.Linq;
using Riok.Mapperly.Abstractions;
using PulsePro.Domain.Entities;
using PulsePro.Application.DTO;

namespace PulsePro.Application.Mappers;

[Mapper]
public partial class ApplicationMapper
{
    // ---------- User ----------
    public partial UserDto UserToDto(User entity);
    public partial User    RegisterUserDtoToUser(RegisterUserDto dto);

    // ---------- Training ----------
    public partial TrainingPlanDto     TrainingPlanToDto(TrainingPlan entity);
    public partial TrainingExerciseDto TrainingExerciseToDto(TrainingExercise entity);

    //  ❯❯  тепер звичайний public‑метод
    public void UpdateTrainingPlanFromDto(TrainingPlanDto dto, TrainingPlan entity)
    {
        entity.PlanName            = dto.PlanName;
        entity.WorkoutsPerWeek     = dto.WorkoutsPerWeek;
        entity.SessionLengthMinutes= dto.SessionLengthMinutes;

        var dtoIds   = dto.Exercises.Select(e => e.Id).ToHashSet();
        var toRemove = entity.Exercises.Where(e => !dtoIds.Contains(e.Id)).ToList();
        foreach (var ex in toRemove) entity.Exercises.Remove(ex);

        foreach (var exDto in dto.Exercises)
        {
            var exEntity = entity.Exercises.FirstOrDefault(e => e.Id == exDto.Id);
            if (exEntity == null)
            {
                exEntity = new TrainingExercise
                {
                    Id             = exDto.Id == Guid.Empty ? Guid.NewGuid() : exDto.Id,
                    TrainingPlanId = entity.Id
                };
                entity.Exercises.Add(exEntity);
            }

            exEntity.MuscleGroup  = exDto.MuscleGroup;
            exEntity.ExerciseName = exDto.ExerciseName;
            exEntity.Sets         = exDto.Sets;
            exEntity.Reps         = exDto.Reps;
            exEntity.StartWeight  = exDto.StartWeight;
            exEntity.TargetWeight = exDto.TargetWeight;
            exEntity.ExerciseTime = exDto.ExerciseTime;
        }
    }

    // ---------- Nutrition ----------
    public partial NutritionDayDto NutritionDayToDto(NutritionDay entity);
    public partial FoodEntryDto    FoodEntryToDto(FoodEntry entity);

    public void UpdateNutritionDayFromDto(NutritionDayDto dto, NutritionDay entity)
    {
        entity.TargetCalories = dto.TargetCalories;
        entity.TargetProtein  = dto.TargetProtein;
        entity.TargetFat      = dto.TargetFat;
        entity.TargetCarbs    = dto.TargetCarbs;

        var dtoIds   = dto.FoodEntries.Select(f => f.Id).ToHashSet();
        var toRemove = entity.FoodEntries.Where(f => !dtoIds.Contains(f.Id)).ToList();
        foreach (var f in toRemove) entity.FoodEntries.Remove(f);

        foreach (var fDto in dto.FoodEntries)
        {
            var fEntity = entity.FoodEntries.FirstOrDefault(f => f.Id == fDto.Id);
            if (fEntity == null)
            {
                fEntity = new FoodEntry
                {
                    Id = fDto.Id == Guid.Empty ? Guid.NewGuid() : fDto.Id,
                    NutritionDayId = entity.Id
                };
                entity.FoodEntries.Add(fEntity);
            }

            fEntity.FoodName = fDto.FoodName;
            fEntity.Calories = fDto.Calories;
            fEntity.Protein  = fDto.Protein;
            fEntity.Fat      = fDto.Fat;
            fEntity.Carbs    = fDto.Carbs;
            fEntity.Fiber    = fDto.Fiber;
        }
    }

    // ---------- Progress ----------
    public partial ProgressRecordDto ProgressRecordToDto(ProgressRecord entity);
}
