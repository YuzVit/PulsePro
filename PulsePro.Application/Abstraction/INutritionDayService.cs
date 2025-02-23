using PulsePro.Application.DTO;

namespace PulsePro.Application.Abstraction
{
    public interface INutritionDayService
    {
        Task<NutritionDayDto> CreateDayAsync(Guid userId, CreateNutritionDayDto dto);
        Task<IEnumerable<NutritionDayDto>> GetDaysForUserAsync(Guid userId);
        Task<NutritionDayDto?> GetDayByIdAsync(Guid dayId);
        Task UpdateDayAsync(NutritionDayDto dto);
        Task DeleteDayAsync(Guid dayId);
    }
}