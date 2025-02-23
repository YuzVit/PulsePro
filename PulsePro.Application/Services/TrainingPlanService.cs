using Microsoft.EntityFrameworkCore;
using PulsePro.Application.Abstraction;
using PulsePro.Application.DTO;
using PulsePro.Application.Mappers;
using PulsePro.Domain.Entities;

namespace PulsePro.Application.Services
{
    public class TrainingPlanService : ITrainingPlanService
    {
        private readonly IApplicationDbContext _db;
        private readonly ApplicationMapper _mapper;

        public TrainingPlanService(IApplicationDbContext db, ApplicationMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<TrainingPlanDto> CreatePlanAsync(Guid userId, CreateTrainingPlanDto dto)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null) throw new Exception("User not found.");

            var plan = new TrainingPlan
            {
                Id = Guid.NewGuid(),
                PlanName = dto.PlanName,
                WorkoutsPerWeek = dto.WorkoutsPerWeek,
                SessionLengthMinutes = dto.SessionLengthMinutes,
                UserId = userId
            };

            _db.TrainingPlans.Add(plan);
            await _db.SaveChangesAsync();

            var planDto = _mapper.TrainingPlanToDto(plan);
            return planDto;
        }

        public async Task<IEnumerable<TrainingPlanDto>> GetPlansForUserAsync(Guid userId)
        {
            var plans = await _db.TrainingPlans
                .Include(p => p.Exercises)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return plans.Select(_mapper.TrainingPlanToDto);
        }

        public async Task<TrainingPlanDto?> GetPlanByIdAsync(Guid planId)
        {
            var plan = await _db.TrainingPlans
                .Include(p => p.Exercises)
                .FirstOrDefaultAsync(p => p.Id == planId);

            return plan == null ? null : _mapper.TrainingPlanToDto(plan);
        }

        public async Task UpdatePlanAsync(TrainingPlanDto dto)
        {
            var plan = await _db.TrainingPlans
                .Include(p => p.Exercises)
                .FirstOrDefaultAsync(p => p.Id == dto.Id);

            if (plan == null) throw new Exception("Plan not found.");

            _mapper.UpdateTrainingPlanFromDto(dto, ref plan);

            // Якщо потрібна окрема логіка оновлення/видалення/додавання exercises:
            // - зіставити dto.Exercises з plan.Exercises

            await _db.SaveChangesAsync();
        }

        public async Task DeletePlanAsync(Guid planId)
        {
            var plan = await _db.TrainingPlans.FindAsync(planId);
            if (plan == null) throw new Exception("Plan not found.");

            _db.TrainingPlans.Remove(plan);
            await _db.SaveChangesAsync();
        }
    }
}
