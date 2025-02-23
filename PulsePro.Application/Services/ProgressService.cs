using Microsoft.EntityFrameworkCore;
using PulsePro.Application.Abstraction;
using PulsePro.Application.DTO;
using PulsePro.Application.Mappers;
using PulsePro.Domain.Entities;

namespace PulsePro.Application.Services
{
    public class ProgressService : IProgressService
    {
        private readonly IApplicationDbContext _db;
        private readonly ApplicationMapper _mapper;

        public ProgressService(IApplicationDbContext db, ApplicationMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProgressRecordDto> CreateRecordAsync(Guid userId, CreateProgressRecordDto dto)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null) throw new Exception("User not found.");

            var record = new ProgressRecord
            {
                Id = Guid.NewGuid(),
                Date = dto.Date,
                CurrentWeight = dto.CurrentWeight,
                WeightChangeSinceLast = dto.WeightChangeSinceLast,
                ExercisesPlanned = dto.ExercisesPlanned,
                ExercisesCompleted = dto.ExercisesCompleted,
                TotalSetsPlanned = dto.TotalSetsPlanned,
                TotalSetsCompleted = dto.TotalSetsCompleted,
                TrainingCompliancePercentage = dto.TrainingCompliancePercentage,
                TrainingNotes = dto.TrainingNotes,
                PlannedCalories = dto.PlannedCalories,
                ActualCalories = dto.ActualCalories,
                PlannedProtein = dto.PlannedProtein,
                ActualProtein = dto.ActualProtein,
                PlannedFat = dto.PlannedFat,
                ActualFat = dto.ActualFat,
                PlannedCarbs = dto.PlannedCarbs,
                ActualCarbs = dto.ActualCarbs,
                NutritionCompliancePercentage = dto.NutritionCompliancePercentage,
                NutritionNotes = dto.NutritionNotes,
                GeneralNotes = dto.GeneralNotes,
                UserId = userId
            };

            _db.ProgressRecords.Add(record);
            await _db.SaveChangesAsync();

            return _mapper.ProgressRecordToDto(record);
        }

        public async Task<IEnumerable<ProgressRecordDto>> GetRecordsForUserAsync(Guid userId)
        {
            var records = await _db.ProgressRecords
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.Date)
                .ToListAsync();

            return records.Select(_mapper.ProgressRecordToDto);
        }

        public async Task<ProgressRecordDto?> GetRecordByIdAsync(Guid recordId)
        {
            var record = await _db.ProgressRecords.FindAsync(recordId);
            return record == null ? null : _mapper.ProgressRecordToDto(record);
        }

        public async Task UpdateRecordAsync(ProgressRecordDto dto)
        {
            var record = await _db.ProgressRecords.FindAsync(dto.Id);
            if (record == null) throw new Exception("Record not found.");

            record.Date = dto.Date;
            record.CurrentWeight = dto.CurrentWeight;
            record.WeightChangeSinceLast = dto.WeightChangeSinceLast;
            record.ExercisesPlanned = dto.ExercisesPlanned;
            record.ExercisesCompleted = dto.ExercisesCompleted;
            record.TotalSetsPlanned = dto.TotalSetsPlanned;
            record.TotalSetsCompleted = dto.TotalSetsCompleted;
            record.TrainingCompliancePercentage = dto.TrainingCompliancePercentage;
            record.TrainingNotes = dto.TrainingNotes;
            record.PlannedCalories = dto.PlannedCalories;
            record.ActualCalories = dto.ActualCalories;
            record.PlannedProtein = dto.PlannedProtein;
            record.ActualProtein = dto.ActualProtein;
            record.PlannedFat = dto.PlannedFat;
            record.ActualFat = dto.ActualFat;
            record.PlannedCarbs = dto.PlannedCarbs;
            record.ActualCarbs = dto.ActualCarbs;
            record.NutritionCompliancePercentage = dto.NutritionCompliancePercentage;
            record.NutritionNotes = dto.NutritionNotes;
            record.GeneralNotes = dto.GeneralNotes;

            _db.ProgressRecords.Update(record);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRecordAsync(Guid recordId)
        {
            var record = await _db.ProgressRecords.FindAsync(recordId);
            if (record == null) throw new Exception("Record not found.");

            _db.ProgressRecords.Remove(record);
            await _db.SaveChangesAsync();
        }
    }
}
