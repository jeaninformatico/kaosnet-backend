using Api_Kaos_Net.Data;
using Api_Kaos_Net.DTO;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Kaos_Net.Services
{
    public class SubscriptionPlanAccountService : ISubscriptionPlanAccountService
    {
        private readonly KaosnetDbContext _context;

        public SubscriptionPlanAccountService(KaosnetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubscriptionPlanAccountDto>> GetAllAsync()
        {
            return await _context.SubscriptionPlanAccounts
                .Select(a => new SubscriptionPlanAccountDto
                {
                    SubscriptionPlanAccountId = a.SubscriptionPlanAccountId,
                    SubscriptionPlanFk = a.SubscriptionPlanFk,
                    StreamingAccountFk = a.StreamingAccountFk,
                    QuantityAccounts = a.QuantityAccounts,
                    AmountSubTotal = a.AmountSubTotal,
                    SubscriptionPlanAccountStatus = a.SubscriptionPlanAccountStatus,
                    IsActive = a.IsActive == 1,
                    Idsession = a.Idsession
                }).ToListAsync();
        }

        public async Task<SubscriptionPlanAccountDto?> GetByIdAsync(int id)
        {
            var a = await _context.SubscriptionPlanAccounts.FindAsync(id);
            if (a == null) return null;

            return new SubscriptionPlanAccountDto
            {
                SubscriptionPlanAccountId = a.SubscriptionPlanAccountId,
                SubscriptionPlanFk = a.SubscriptionPlanFk,
                StreamingAccountFk = a.StreamingAccountFk,
                QuantityAccounts = a.QuantityAccounts,
                AmountSubTotal = a.AmountSubTotal,
                SubscriptionPlanAccountStatus = a.SubscriptionPlanAccountStatus,
                IsActive = a.IsActive == 1,
                Idsession = a.Idsession
            };
        }

        public async Task<SubscriptionPlanAccountDto> CreateAsync(SubscriptionPlanAccountDto dto)
        {
            var entity = new SubscriptionPlanAccount
            {
                SubscriptionPlanFk = dto.SubscriptionPlanFk,
                StreamingAccountFk = dto.StreamingAccountFk,
                QuantityAccounts = dto.QuantityAccounts,
                AmountSubTotal = dto.AmountSubTotal,
                SubscriptionPlanAccountStatus = dto.SubscriptionPlanAccountStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.SubscriptionPlanAccounts.Add(entity);
            await _context.SaveChangesAsync();

            dto.SubscriptionPlanAccountId = entity.SubscriptionPlanAccountId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, SubscriptionPlanAccountDto dto)
        {
            var entity = await _context.SubscriptionPlanAccounts.FindAsync(id);
            if (entity == null) return false;

            entity.SubscriptionPlanFk = dto.SubscriptionPlanFk;
            entity.StreamingAccountFk = dto.StreamingAccountFk;
            entity.QuantityAccounts = dto.QuantityAccounts;
            entity.AmountSubTotal = dto.AmountSubTotal;
            entity.SubscriptionPlanAccountStatus = dto.SubscriptionPlanAccountStatus;
            entity.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            entity.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SubscriptionPlanAccounts.FindAsync(id);
            if (entity == null) return false;

            _context.SubscriptionPlanAccounts.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
