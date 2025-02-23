using Microsoft.EntityFrameworkCore;
using PulsePro.Application.Abstraction;
using PulsePro.Application.DTO;
using PulsePro.Application.Mappers;
using PulsePro.Domain.Entities;

namespace PulsePro.Application.Services
{
    public class NutritionDayService : INutritionDayService
    {
        private readonly IApplicationDbContext _db;
        private readonly ApplicationMapper _mapper;

        public NutritionDayService(IApplicationDbContext db, ApplicationMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<NutritionDayDto> CreateDayAsync(Guid userId, CreateNutritionDayDto dto)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null) throw new Exception("User not found.");

            var day = new NutritionDay
            {
                Id = Guid.NewGuid(),
                TargetCalories = dto.TargetCalories,
                TargetProtein = dto.TargetProtein,
                TargetFat = dto.TargetFat,
                TargetCarbs = dto.TargetCarbs,
                UserId = userId
            };

            _db.NutritionDays.Add(day);
            await _db.SaveChangesAsync();

            var dayDto = _mapper.NutritionDayToDto(day);
            return dayDto;
        }

        public async Task<IEnumerable<NutritionDayDto>> GetDaysForUserAsync(Guid userId)
        {
            var days = await _db.NutritionDays
                .Include(d => d.FoodEntries)
                .Where(d => d.UserId == userId)
                .ToListAsync();

            return days.Select(_mapper.NutritionDayToDto);
        }

        public async Task<NutritionDayDto?> GetDayByIdAsync(Guid dayId)
        {
            var day = await _db.NutritionDays
                .Include(d => d.FoodEntries)
                .FirstOrDefaultAsync(d => d.Id == dayId);

            return day == null ? null : _mapper.NutritionDayToDto(day);
        }

        public async Task UpdateDayAsync(NutritionDayDto dto)
        {
            var day = await _db.NutritionDays
                .Include(d => d.FoodEntries)
                .FirstOrDefaultAsync(d => d.Id == dto.Id);

            if (day == null) throw new Exception("NutritionDay not found.");

            _mapper.UpdateNutritionDayFromDto(dto, ref day);
            // Окремо можна опрацювати FoodEntries (додавання / оновлення / видалення)

            await _db.SaveChangesAsync();
        }

        public async Task DeleteDayAsync(Guid dayId)
        {
            var day = await _db.NutritionDays.FindAsync(dayId);
            if (day == null) throw new Exception("NutritionDay not found.");

            _db.NutritionDays.Remove(day);
            await _db.SaveChangesAsync();
        }
    }
}
