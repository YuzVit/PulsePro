namespace PulsePro.Domain.Entities;

public class ProgressRecord
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public double CurrentWeight { get; set; }
    public double? WeightChangeSinceLast { get; set; }

    public int ExercisesPlanned { get; set; }
    public int ExercisesCompleted { get; set; }
    public int TotalSetsPlanned { get; set; }
    public int TotalSetsCompleted { get; set; }
    public double? TrainingCompliancePercentage { get; set; }
    public string? TrainingNotes { get; set; }

    public int PlannedCalories { get; set; }
    public int ActualCalories { get; set; }
    public int PlannedProtein { get; set; }
    public int ActualProtein { get; set; }
    public int PlannedFat { get; set; }
    public int ActualFat { get; set; }
    public int PlannedCarbs { get; set; }
    public int ActualCarbs { get; set; }
    public double? NutritionCompliancePercentage { get; set; }
    public string? NutritionNotes { get; set; }

    public string? GeneralNotes { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}