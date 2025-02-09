namespace PulsePro.Domain.Entities;

public class NutritionDay
{
    public Guid Id { get; set; }

    public int TargetCalories { get; set; }
    public int TargetProtein { get; set; }
    public int TargetFat { get; set; }
    public int TargetCarbs { get; set; }

    public ICollection<FoodEntry> FoodEntries { get; set; } = new List<FoodEntry>();

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}