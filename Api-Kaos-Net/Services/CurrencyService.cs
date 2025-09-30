using Api_Kaos_Net.Data;
using Api_Kaos_Net.DTOs;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Kaos_Net.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly KaosnetDbContext _context;

        public CurrencyService(KaosnetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CurrencyDto>> GetAllAsync()
        {
            return await _context.Currencies
                .Select(c => new CurrencyDto
                {
                    CurrencyId = c.CurrencyId,
                    CurrencyName = c.CurrencyName,
                    CurrencyCode = c.CurrencyCode,
                    Symbol = c.Symbol,
                    CurrencyStatus = c.CurrencyStatus,
                    IsActive = c.IsActive == 1,
                    Idsession = c.Idsession
                }).ToListAsync();
        }

        public async Task<CurrencyDto?> GetByIdAsync(int id)
        {
            var c = await _context.Currencies.FindAsync(id);
            if (c == null) return null;

            return new CurrencyDto
            {
                CurrencyId = c.CurrencyId,
                CurrencyName = c.CurrencyName,
                CurrencyCode = c.CurrencyCode,
                Symbol = c.Symbol,
                CurrencyStatus = c.CurrencyStatus,
                IsActive = c.IsActive == 1,
                Idsession = c.Idsession
            };
        }

        public async Task<CurrencyDto> CreateAsync(CurrencyDto dto)
        {
            var entity = new Currency
            {
                CurrencyName = dto.CurrencyName,
                CurrencyCode = dto.CurrencyCode,
                Symbol = dto.Symbol,
                CurrencyStatus = dto.CurrencyStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.Currencies.Add(entity);
            await _context.SaveChangesAsync();

            dto.CurrencyId = entity.CurrencyId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, CurrencyDto dto)
        {
            var entity = await _context.Currencies.FindAsync(id);
            if (entity == null) return false;

            entity.CurrencyName = dto.CurrencyName;
            entity.CurrencyCode = dto.CurrencyCode;
            entity.Symbol = dto.Symbol;
            entity.CurrencyStatus = dto.CurrencyStatus;
            entity.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            entity.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Currencies.FindAsync(id);
            if (entity == null) return false;

            _context.Currencies.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
