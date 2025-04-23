using Microsoft.EntityFrameworkCore;
using PulsePro.Application.Abstraction;
using PulsePro.Application.DTO;
using PulsePro.Application.Mappers;
using PulsePro.Domain.Entities;

namespace PulsePro.Application.Services;

public class ProgressService : IProgressService
{
    private readonly IApplicationDbContext _db;
    private readonly ApplicationMapper     _map;

    public ProgressService(IApplicationDbContext db, ApplicationMapper map)
    {
        _db  = db;
        _map = map;
    }

    public async Task<ProgressRecordDto> CreateRecordAsync(Guid userId, CreateProgressRecordDto dto)
    {
        if (await _db.Users.FindAsync(userId) is null)
            throw new InvalidOperationException("User not found.");

        var entity = new ProgressRecord
        {
            Id = Guid.NewGuid(),
            UserId                       = userId,
            Date                         = dto.Date,
            CurrentWeight                = dto.CurrentWeight,
            WeightChangeSinceLast        = dto.WeightChangeSinceLast,
            ExercisesPlanned             = dto.ExercisesPlanned,
            ExercisesCompleted           = dto.ExercisesCompleted,
            TotalSetsPlanned             = dto.TotalSetsPlanned,
            TotalSetsCompleted           = dto.TotalSetsCompleted,
            TrainingCompliancePercentage = dto.TrainingCompliancePercentage,
            TrainingNotes                = dto.TrainingNotes,
            PlannedCalories              = dto.PlannedCalories,
            ActualCalories               = dto.ActualCalories,
            PlannedProtein               = dto.PlannedProtein,
            ActualProtein                = dto.ActualProtein,
            PlannedFat                   = dto.PlannedFat,
            ActualFat                    = dto.ActualFat,
            PlannedCarbs                 = dto.PlannedCarbs,
            ActualCarbs                  = dto.ActualCarbs,
            NutritionCompliancePercentage= dto.NutritionCompliancePercentage,
            NutritionNotes               = dto.NutritionNotes,
            GeneralNotes                 = dto.GeneralNotes
        };

        _db.ProgressRecords.Add(entity);
        await _db.SaveChangesAsync();

        return _map.ProgressRecordToDto(entity);
    }

    public async Task<IEnumerable<ProgressRecordDto>> GetRecordsForUserAsync(Guid userId)
    {
        var list = await _db.ProgressRecords
                            .Where(r => r.UserId == userId)
                            .OrderByDescending(r => r.Date)
                            .ToListAsync();
        return list.Select(_map.ProgressRecordToDto);
    }

    public async Task<ProgressRecordDto?> GetRecordByIdAsync(Guid id)
    {
        var entity = await _db.ProgressRecords.FindAsync(id);
        return entity is null ? null : _map.ProgressRecordToDto(entity);
    }

    public async Task UpdateRecordAsync(ProgressRecordDto dto)
    {
        var entity = await _db.ProgressRecords.FindAsync(dto.Id)
                     ?? throw new InvalidOperationException("Record not found.");

        entity.Date                         = dto.Date;
        entity.CurrentWeight                = dto.CurrentWeight;
        entity.WeightChangeSinceLast        = dto.WeightChangeSinceLast;
        entity.ExercisesPlanned             = dto.ExercisesPlanned;
        entity.ExercisesCompleted           = dto.ExercisesCompleted;
        entity.TotalSetsPlanned             = dto.TotalSetsPlanned;
        entity.TotalSetsCompleted           = dto.TotalSetsCompleted;
        entity.TrainingCompliancePercentage = dto.TrainingCompliancePercentage;
        entity.TrainingNotes                = dto.TrainingNotes;
        entity.PlannedCalories              = dto.PlannedCalories;
        entity.ActualCalories               = dto.ActualCalories;
        entity.PlannedProtein               = dto.PlannedProtein;
        entity.ActualProtein                = dto.ActualProtein;
        entity.PlannedFat                   = dto.PlannedFat;
        entity.ActualFat                    = dto.ActualFat;
        entity.PlannedCarbs                 = dto.PlannedCarbs;
        entity.ActualCarbs                  = dto.ActualCarbs;
        entity.NutritionCompliancePercentage= dto.NutritionCompliancePercentage;
        entity.NutritionNotes               = dto.NutritionNotes;
        entity.GeneralNotes                 = dto.GeneralNotes;

        await _db.SaveChangesAsync();
    }

    public async Task DeleteRecordAsync(Guid id)
    {
        var entity = await _db.ProgressRecords.FindAsync(id)
                     ?? throw new InvalidOperationException("Record not found.");

        _db.ProgressRecords.Remove(entity);
        await _db.SaveChangesAsync();
    }
}
