namespace PulsePro.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public double Weight { get; set; }
    public double Height { get; set; }
    public DateTime BirthDate { get; set; }
    
    public ICollection<TrainingPlan> TrainingPlans { get; set; } = new List<TrainingPlan>();
    public ICollection<NutritionDay> NutritionDays { get; set; } = new List<NutritionDay>();
    public ICollection<ProgressRecord> ProgressRecords { get; set; } = new List<ProgressRecord>();
}