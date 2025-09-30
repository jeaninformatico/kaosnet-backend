using Api_Kaos_Net.Data;
using Api_Kaos_Net.DTOs;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Kaos_Net.Services
{
    public class SalesAccountService : ISalesAccountService
    {
        private readonly KaosnetDbContext _context;

        public SalesAccountService(KaosnetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesAccountDto>> GetAllAsync()
        {
            return await _context.SalesAccounts
                .Select(s => new SalesAccountDto
                {
                    IdSalesAccount = s.IdSalesAccount,
                    ContactNumber = s.ContactNumber,
                    ContactEmail = s.ContactEmail,
                    SalesDate = s.SalesDate,
                    ProfilePin = s.ProfilePin,
                    ProfileNumber = s.ProfileNumber,
                    AmountSales = s.AmountSales,
                    CustomerId = s.CustomerId,
                    StreamingAccountFk = s.StreamingAccountFk,
                    CurrencyFk = s.CurrencyFk,
                    SalesAccountStatus = s.SalesAccountStatus,
                    IsActive = s.IsActive == 1,
                    Idsession = s.Idsession
                }).ToListAsync();
        }

        public async Task<SalesAccountDto?> GetByIdAsync(int id)
        {
            var s = await _context.SalesAccounts.FindAsync(id);
            if (s == null) return null;

            return new SalesAccountDto
            {
                IdSalesAccount = s.IdSalesAccount,
                ContactNumber = s.ContactNumber,
                ContactEmail = s.ContactEmail,
                SalesDate = s.SalesDate,
                ProfilePin = s.ProfilePin,
                ProfileNumber = s.ProfileNumber,
                AmountSales = s.AmountSales,
                CustomerId = s.CustomerId,
                StreamingAccountFk = s.StreamingAccountFk,
                CurrencyFk = s.CurrencyFk,
                SalesAccountStatus = s.SalesAccountStatus,
                IsActive = s.IsActive == 1,
                Idsession = s.Idsession
            };
        }

        public async Task<SalesAccountDto> CreateAsync(SalesAccountDto dto)
        {
            var entity = new SalesAccount
            {
                ContactNumber = dto.ContactNumber,
                ContactEmail = dto.ContactEmail,
                SalesDate = dto.SalesDate,
                ProfilePin = dto.ProfilePin,
                ProfileNumber = dto.ProfileNumber,
                AmountSales = dto.AmountSales,
                CustomerId = dto.CustomerId,
                StreamingAccountFk = dto.StreamingAccountFk,
                CurrencyFk = dto.CurrencyFk,
                SalesAccountStatus = dto.SalesAccountStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.SalesAccounts.Add(entity);
            await _context.SaveChangesAsync();

            dto.IdSalesAccount = entity.IdSalesAccount;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, SalesAccountDto dto)
        {
            var entity = await _context.SalesAccounts.FindAsync(id);
            if (entity == null) return false;

            entity.ContactNumber = dto.ContactNumber;
            entity.ContactEmail = dto.ContactEmail;
            entity.SalesDate = dto.SalesDate;
            entity.ProfilePin = dto.ProfilePin;
            entity.ProfileNumber = dto.ProfileNumber;
            entity.AmountSales = dto.AmountSales;
            entity.CustomerId = dto.CustomerId;
            entity.StreamingAccountFk = dto.StreamingAccountFk;
            entity.CurrencyFk = dto.CurrencyFk;
            entity.SalesAccountStatus = dto.SalesAccountStatus;
            entity.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            entity.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SalesAccounts.FindAsync(id);
            if (entity == null) return false;

            _context.SalesAccounts.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
