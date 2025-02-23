namespace PulsePro.Domain.Entities;

public class TrainingPlan
{
    public Guid Id { get; set; }
    public string PlanName { get; set; } = null!;          

    public int WorkoutsPerWeek { get; set; }               
    public int SessionLengthMinutes { get; set; }          
          

    public ICollection<TrainingExercise> Exercises { get; set; } = new List<TrainingExercise>();

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}