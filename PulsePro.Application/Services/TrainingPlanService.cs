using Microsoft.EntityFrameworkCore;
using PulsePro.Application.Abstraction;
using PulsePro.Application.DTO;
using PulsePro.Application.Mappers;
using PulsePro.Domain.Entities;

namespace PulsePro.Application.Services;

public class TrainingPlanService : ITrainingPlanService
{
    private readonly IApplicationDbContext _db;
    private readonly ApplicationMapper     _map;

    public TrainingPlanService(IApplicationDbContext db, ApplicationMapper map)
    {
        _db  = db;
        _map = map;
    }

    public async Task<TrainingPlanDto> CreatePlanAsync(Guid userId, CreateTrainingPlanDto dto)
    {
        if (await _db.Users.FindAsync(userId) is null)
            throw new InvalidOperationException("User not found.");

        var entity = new TrainingPlan
        {
            Id                   = Guid.NewGuid(),
            UserId               = userId,
            PlanName             = dto.PlanName,
            WorkoutsPerWeek      = dto.WorkoutsPerWeek,
            SessionLengthMinutes = dto.SessionLengthMinutes
        };

        _db.TrainingPlans.Add(entity);
        await _db.SaveChangesAsync();

        return _map.TrainingPlanToDto(entity);
    }

    public async Task<IEnumerable<TrainingPlanDto>> GetPlansForUserAsync(Guid userId)
    {
        var plans = await _db.TrainingPlans
                             .Include(p => p.Exercises)
                             .Where(p => p.UserId == userId)
                             .ToListAsync();
        return plans.Select(_map.TrainingPlanToDto);
    }

    public async Task<TrainingPlanDto?> GetPlanByIdAsync(Guid id)
    {
        var plan = await _db.TrainingPlans
                            .Include(p => p.Exercises)
                            .FirstOrDefaultAsync(p => p.Id == id);
        return plan is null ? null : _map.TrainingPlanToDto(plan);
    }

    public async Task UpdatePlanAsync(TrainingPlanDto dto)
    {
        var entity = await _db.TrainingPlans
                              .Include(p => p.Exercises)
                              .FirstOrDefaultAsync(p => p.Id == dto.Id)
                     ?? throw new InvalidOperationException("Plan not found.");

        _map.UpdateTrainingPlanFromDto(dto, entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeletePlanAsync(Guid id)
    {
        var plan = await _db.TrainingPlans.FindAsync(id)
                   ?? throw new InvalidOperationException("Plan not found.");

        _db.TrainingPlans.Remove(plan);
        await _db.SaveChangesAsync();
    }
}
