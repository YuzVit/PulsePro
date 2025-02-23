using PulsePro.Application.DTO;

namespace PulsePro.Application.Abstraction
{
    public interface ITrainingPlanService
    {
        Task<TrainingPlanDto> CreatePlanAsync(Guid userId, CreateTrainingPlanDto dto);
        Task<IEnumerable<TrainingPlanDto>> GetPlansForUserAsync(Guid userId);
        Task<TrainingPlanDto?> GetPlanByIdAsync(Guid planId);
        Task UpdatePlanAsync(TrainingPlanDto dto);
        Task DeletePlanAsync(Guid planId);
    }
}