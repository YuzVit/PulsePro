using PulsePro.Application.DTO;

namespace PulsePro.Application.Abstraction
{
    public interface IProgressService
    {
        Task<ProgressRecordDto> CreateRecordAsync(Guid userId, CreateProgressRecordDto dto);
        Task<IEnumerable<ProgressRecordDto>> GetRecordsForUserAsync(Guid userId);
        Task<ProgressRecordDto?> GetRecordByIdAsync(Guid recordId);
        Task UpdateRecordAsync(ProgressRecordDto dto);
        Task DeleteRecordAsync(Guid recordId);
    }
}