namespace PulsePro.Domain.Entities;

public class TrainingExercise
{
    public Guid Id { get; set; }
    
    public string MuscleGroup { get; set; } = null!;       
    public string ExerciseName { get; set; } = null!;      
    public int Sets { get; set; }                          
    public int Reps { get; set; }                          

    public Guid TrainingPlanId { get; set; }
    public TrainingPlan TrainingPlan { get; set; } = null!;
}