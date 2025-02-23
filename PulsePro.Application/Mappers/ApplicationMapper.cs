using Riok.Mapperly.Abstractions;
using PulsePro.Domain.Entities;
using PulsePro.Application.DTO;
using System.Collections.Generic;

namespace PulsePro.Application.Mappers
{
    [Mapper]
    public partial class ApplicationMapper
    {
        // ---------------- User ----------------
        public partial UserDto UserToDto(User user);
        public partial User RegisterUserDtoToUser(RegisterUserDto dto);

        // ---------------- TrainingPlan & Exercise ----------------
        public partial TrainingPlanDto TrainingPlanToDto(TrainingPlan plan);
        public partial void UpdateTrainingPlanFromDto(TrainingPlanDto dto, ref TrainingPlan plan);

        public partial TrainingExerciseDto TrainingExerciseToDto(TrainingExercise exercise);

        // ---------------- FoodEntry ----------------
        public partial FoodEntryDto FoodEntryToDto(FoodEntry entry);

        // ---------------- NutritionDay ----------------
        public partial NutritionDayDto NutritionDayToDto(NutritionDay day);
        public partial void UpdateNutritionDayFromDto(NutritionDayDto dto, ref NutritionDay day);

        // ---------------- ProgressRecord ----------------
        public partial ProgressRecordDto ProgressRecordToDto(ProgressRecord record);
    }
}