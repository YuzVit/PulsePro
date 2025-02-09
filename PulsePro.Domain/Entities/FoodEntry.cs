namespace PulsePro.Domain.Entities;

public class FoodEntry
{
    public Guid Id { get; set; }
    public string FoodName { get; set; } = null!;          

    public int Calories { get; set; }
    public int Protein { get; set; }
    public int Fat { get; set; }
    public int Carbs { get; set; }

    public Guid NutritionDayId { get; set; }
    public NutritionDay NutritionDay { get; set; } = null!;
}