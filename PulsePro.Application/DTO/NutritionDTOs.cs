namespace PulsePro.Application.DTO
{
    public class CreateNutritionDayDto
    {
        public int TargetCalories { get; set; }
        public int TargetProtein { get; set; }
        public int TargetFat { get; set; }
        public int TargetCarbs { get; set; }
    }

    public class NutritionDayDto
    {
        public Guid Id { get; set; }
        public int TargetCalories { get; set; }
        public int TargetProtein { get; set; }
        public int TargetFat { get; set; }
        public int TargetCarbs { get; set; }
        public List<FoodEntryDto> FoodEntries { get; set; } = new();
    }

    public class CreateFoodEntryDto
    {
        public string FoodName { get; set; } = null!;
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int Fiber { get; set; }
    }

    public class FoodEntryDto
    {
        public Guid Id { get; set; }
        public string FoodName { get; set; } = null!;
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int Fiber { get; set; }
    }
}