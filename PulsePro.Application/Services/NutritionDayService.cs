using Microsoft.EntityFrameworkCore;
using PulsePro.Application.Abstraction;
using PulsePro.Application.DTO;
using PulsePro.Application.Mappers;
using PulsePro.Domain.Entities;

namespace PulsePro.Application.Services;

public class NutritionDayService : INutritionDayService
{
    private readonly IApplicationDbContext _db;
    private readonly ApplicationMapper     _map;

    public NutritionDayService(IApplicationDbContext db, ApplicationMapper map)
    {
        _db  = db;
        _map = map;
    }

    public async Task<NutritionDayDto> CreateDayAsync(Guid userId, CreateNutritionDayDto dto)
    {
        if (await _db.Users.FindAsync(userId) is null)
            throw new InvalidOperationException("User not found.");

        var entity = new NutritionDay
        {
            Id            = Guid.NewGuid(),
            UserId        = userId,
            TargetCalories= dto.TargetCalories,
            TargetProtein = dto.TargetProtein,
            TargetFat     = dto.TargetFat,
            TargetCarbs   = dto.TargetCarbs
        };

        _db.NutritionDays.Add(entity);
        await _db.SaveChangesAsync();

        return _map.NutritionDayToDto(entity);
    }

    public async Task<IEnumerable<NutritionDayDto>> GetDaysForUserAsync(Guid userId)
    {
        var list = await _db.NutritionDays
                            .Include(d => d.FoodEntries)
                            .Where(d => d.UserId == userId)
                            .ToListAsync();
        return list.Select(_map.NutritionDayToDto);
    }

    public async Task<NutritionDayDto?> GetDayByIdAsync(Guid id)
    {
        var entity = await _db.NutritionDays
                              .Include(d => d.FoodEntries)
                              .FirstOrDefaultAsync(d => d.Id == id);
        return entity is null ? null : _map.NutritionDayToDto(entity);
    }

    public async Task UpdateDayAsync(NutritionDayDto dto)
    {
        var entity = await _db.NutritionDays
                              .Include(d => d.FoodEntries)
                              .FirstOrDefaultAsync(d => d.Id == dto.Id)
                     ?? throw new InvalidOperationException("Day not found.");

        _map.UpdateNutritionDayFromDto(dto, entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteDayAsync(Guid id)
    {
        var day = await _db.NutritionDays.FindAsync(id)
                  ?? throw new InvalidOperationException("Day not found.");

        _db.NutritionDays.Remove(day);
        await _db.SaveChangesAsync();
    }
}
