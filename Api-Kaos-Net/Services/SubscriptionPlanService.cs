using Api_Kaos_Net.Data;
using Api_Kaos_Net.DTOs;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Kaos_Net.Services
{
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private readonly KaosnetDbContext _context;

        public SubscriptionPlanService(KaosnetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubscriptionPlanDto>> GetAllAsync()
        {
            return await _context.SubscriptionPlans
                .Select(p => new SubscriptionPlanDto
                {
                    SubscriptionPlanId = p.SubscriptionPlanId,
                    SubscriptionPlanName = p.SubscriptionPlanName,
                    SubscriptionPlanDescription = p.SubscriptionPlanDescription,
                    AmountTotal = p.AmountTotal,
                    SubscriptionPlanStatus = p.SubscriptionPlanStatus,
                    IsActive = p.IsActive == 1,
                    Idsession = p.Idsession
                }).ToListAsync();
        }

        public async Task<SubscriptionPlanDto?> GetByIdAsync(int id)
        {
            var p = await _context.SubscriptionPlans.FindAsync(id);
            if (p == null) return null;

            return new SubscriptionPlanDto
            {
                SubscriptionPlanId = p.SubscriptionPlanId,
                SubscriptionPlanName = p.SubscriptionPlanName,
                SubscriptionPlanDescription = p.SubscriptionPlanDescription,
                AmountTotal = p.AmountTotal,
                SubscriptionPlanStatus = p.SubscriptionPlanStatus,
                IsActive = p.IsActive == 1,
                Idsession = p.Idsession
            };
        }

        public async Task<SubscriptionPlanDto> CreateAsync(SubscriptionPlanDto dto)
        {
            var entity = new SubscriptionPlan
            {
                SubscriptionPlanName = dto.SubscriptionPlanName,
                SubscriptionPlanDescription = dto.SubscriptionPlanDescription,
                AmountTotal = dto.AmountTotal,
                SubscriptionPlanStatus = dto.SubscriptionPlanStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.SubscriptionPlans.Add(entity);
            await _context.SaveChangesAsync();

            dto.SubscriptionPlanId = entity.SubscriptionPlanId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, SubscriptionPlanDto dto)
        {
            var entity = await _context.SubscriptionPlans.FindAsync(id);
            if (entity == null) return false;

            entity.SubscriptionPlanName = dto.SubscriptionPlanName;
            entity.SubscriptionPlanDescription = dto.SubscriptionPlanDescription;
            entity.AmountTotal = dto.AmountTotal;
            entity.SubscriptionPlanStatus = dto.SubscriptionPlanStatus;
            entity.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            entity.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SubscriptionPlans.FindAsync(id);
            if (entity == null) return false;

            _context.SubscriptionPlans.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
