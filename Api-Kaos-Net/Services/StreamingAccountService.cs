using Api_Kaos_Net.Data;
using Api_Kaos_Net.DTOs;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Kaos_Net.Services
{
    public class StreamingAccountService : IStreamingAccountService
    {
        private readonly KaosnetDbContext _context;

        public StreamingAccountService(KaosnetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StreamingAccountDto>> GetAllAsync()
        {
            return await _context.StreamingAccounts
                .Select(a => new StreamingAccountDto
                {
                    StreamingAccountId = a.StreamingAccountId,
                    StreamingAccountName = a.StreamingAccountName,
                    AccountEmail = a.AccountEmail,
                    AccountPassword = a.AccountPassword,
                    Price = a.Price,
                    StreamingTypeFk = a.StreamingTypeFk,
                    ValidDate = a.ValidDate,
                    ExpiredDate = a.ExpiredDate,
                    MaximumQuantityProfiles = a.MaximumQuantityProfiles,
                    ProfilesQuantity = a.ProfilesQuantity,
                    StreamingAccountStatus = a.StreamingAccountStatus,
                    IsActive = a.IsActive == 1,
                    Idsession = a.Idsession
                }).ToListAsync();
        }

        public async Task<StreamingAccountDto?> GetByIdAsync(int id)
        {
            var a = await _context.StreamingAccounts.FindAsync(id);
            if (a == null) return null;

            return new StreamingAccountDto
            {
                StreamingAccountId = a.StreamingAccountId,
                StreamingAccountName = a.StreamingAccountName,
                AccountEmail = a.AccountEmail,
                AccountPassword = a.AccountPassword,
                Price = a.Price,
                StreamingTypeFk = a.StreamingTypeFk,
                ValidDate = a.ValidDate,
                ExpiredDate = a.ExpiredDate,
                MaximumQuantityProfiles = a.MaximumQuantityProfiles,
                ProfilesQuantity = a.ProfilesQuantity,
                StreamingAccountStatus = a.StreamingAccountStatus,
                IsActive = a.IsActive == 1,
                Idsession = a.Idsession
            };
        }

        public async Task<StreamingAccountDto> CreateAsync(StreamingAccountDto dto)
        {
            var entity = new StreamingAccount
            {
                StreamingAccountName = dto.StreamingAccountName,
                AccountEmail = dto.AccountEmail,
                AccountPassword = dto.AccountPassword,
                Price = dto.Price,
                StreamingTypeFk = dto.StreamingTypeFk,
                ValidDate = dto.ValidDate,
                ExpiredDate = dto.ExpiredDate,
                MaximumQuantityProfiles = dto.MaximumQuantityProfiles,
                ProfilesQuantity = dto.ProfilesQuantity,
                StreamingAccountStatus = dto.StreamingAccountStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.StreamingAccounts.Add(entity);
            await _context.SaveChangesAsync();

            dto.StreamingAccountId = entity.StreamingAccountId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, StreamingAccountDto dto)
        {
            var entity = await _context.StreamingAccounts.FindAsync(id);
            if (entity == null) return false;

            entity.StreamingAccountName = dto.StreamingAccountName;
            entity.AccountEmail = dto.AccountEmail;
            entity.AccountPassword = dto.AccountPassword;
            entity.Price = dto.Price;
            entity.StreamingTypeFk = dto.StreamingTypeFk;
            entity.ValidDate = dto.ValidDate;
            entity.ExpiredDate = dto.ExpiredDate;
            entity.MaximumQuantityProfiles = dto.MaximumQuantityProfiles;
            entity.ProfilesQuantity = dto.ProfilesQuantity;
            entity.StreamingAccountStatus = dto.StreamingAccountStatus;
            entity.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            entity.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.StreamingAccounts.FindAsync(id);
            if (entity == null) return false;

            _context.StreamingAccounts.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
