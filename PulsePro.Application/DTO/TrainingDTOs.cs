namespace PulsePro.Application.DTO
{
    public class CreateTrainingPlanDto
    {
        public string PlanName { get; set; } = null!;
        public int WorkoutsPerWeek { get; set; }
        public int SessionLengthMinutes { get; set; }
    }

    public class TrainingPlanDto
    {
        public Guid Id { get; set; }
        public string PlanName { get; set; } = null!;
        public int WorkoutsPerWeek { get; set; }
        public int SessionLengthMinutes { get; set; }
        public List<TrainingExerciseDto> Exercises { get; set; } = new();
    }

    public class CreateTrainingExerciseDto
    {
        public string MuscleGroup { get; set; } = null!;
        public string ExerciseName { get; set; } = null!;
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double? StartWeight { get; set; }
        public double? TargetWeight { get; set; }
        public int? ExerciseTime { get; set; }
    }

    public class TrainingExerciseDto
    {
        public Guid Id { get; set; }
        public string MuscleGroup { get; set; } = null!;
        public string ExerciseName { get; set; } = null!;
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double? StartWeight { get; set; }
        public double? TargetWeight { get; set; }
        public int? ExerciseTime { get; set; }
    }
}