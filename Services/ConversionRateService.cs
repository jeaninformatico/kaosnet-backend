using Api_Kaos_Net.Data;
using Api_Kaos_Net.DTOs;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Kaos_Net.Services
{
    public class ConversionRateService : IConversionRateService
    {
        private readonly KaosnetDbContext _context;

        public ConversionRateService(KaosnetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConversionRateDto>> GetAllAsync()
        {
            return await _context.ConversionRates
                .Select(r => new ConversionRateDto
                {
                    ConversionRateId = r.ConversionRateId,
                    ValidDate = r.ValidDate,
                    AmountRate = r.AmountRate,
                    CurrencyFromId = r.CurrencyFromId,
                    CurrencyToId = r.CurrencyToId,
                    IsReversed = r.IsReversed == 1,
                    ParentConversionRateFk = r.ParentConversionRateFk,
                    ConversionRateStatus = r.ConversionRateStatus,
                    IsActive = r.IsActive == 1,
                    Idsession = r.Idsession
                }).ToListAsync();
        }

        public async Task<ConversionRateDto?> GetByIdAsync(int id)
        {
            var r = await _context.ConversionRates.FindAsync(id);
            if (r == null) return null;

            return new ConversionRateDto
            {
                ConversionRateId = r.ConversionRateId,
                ValidDate = r.ValidDate,
                AmountRate = r.AmountRate,
                CurrencyFromId = r.CurrencyFromId,
                CurrencyToId = r.CurrencyToId,
                IsReversed = r.IsReversed == 1,
                ParentConversionRateFk = r.ParentConversionRateFk,
                ConversionRateStatus = r.ConversionRateStatus,
                IsActive = r.IsActive == 1,
                Idsession = r.Idsession
            };
        }

        public async Task<ConversionRateDto> CreateAsync(ConversionRateDto dto)
        {
            var entity = new ConversionRate
            {
                ValidDate = dto.ValidDate,
                AmountRate = dto.AmountRate,
                CurrencyFromId = dto.CurrencyFromId,
                CurrencyToId = dto.CurrencyToId,
                IsReversed = (sbyte?)(dto.IsReversed == true ? 1 : 0),
                ParentConversionRateFk = dto.ParentConversionRateFk,
                ConversionRateStatus = dto.ConversionRateStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.ConversionRates.Add(entity);
            await _context.SaveChangesAsync();

            dto.ConversionRateId = entity.ConversionRateId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, ConversionRateDto dto)
        {
            var entity = await _context.ConversionRates.FindAsync(id);
            if (entity == null) return false;

            entity.ValidDate = dto.ValidDate;
            entity.AmountRate = dto.AmountRate;
            entity.CurrencyFromId = dto.CurrencyFromId;
            entity.CurrencyToId = dto.CurrencyToId;
            entity.IsReversed = (sbyte?)(dto.IsReversed == true ? 1 : 0);
            entity.ParentConversionRateFk = dto.ParentConversionRateFk;
            entity.ConversionRateStatus = dto.ConversionRateStatus;
            entity.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            entity.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ConversionRates.FindAsync(id);
            if (entity == null) return false;

            _context.ConversionRates.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
