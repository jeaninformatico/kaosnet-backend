using Api_Kaos_Net.Data;
using Api_Kaos_Net.DTOs;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Kaos_Net.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly KaosnetDbContext _context;

        public PasswordService(KaosnetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PasswordDto>> GetAllAsync()
        {
            return await _context.Passwords
                .Select(p => new PasswordDto
                {
                    PasswordId = p.PasswordId,
                    Password1 = p.Password1,
                    CreatedDate = p.CreatedDate,
                    ExpirationDate = p.ExpirationDate,
                    UserFk = p.UserFk,
                    PasswordStatus = p.PasswordStatus,
                    IsActive = p.IsActive == 1,
                    Idsession = p.Idsession
                }).ToListAsync();
        }

        public async Task<PasswordDto?> GetByIdAsync(int id)
        {
            var p = await _context.Passwords.FindAsync(id);
            if (p == null) return null;

            return new PasswordDto
            {
                PasswordId = p.PasswordId,
                Password1 = p.Password1,
                CreatedDate = p.CreatedDate,
                ExpirationDate = p.ExpirationDate,
                UserFk = p.UserFk,
                PasswordStatus = p.PasswordStatus,
                IsActive = p.IsActive == 1,
                Idsession = p.Idsession
            };
        }

        public async Task<PasswordDto> CreateAsync(PasswordDto dto)
        {
            var entity = new Password
            {
                Password1 = dto.Password1,
                CreatedDate = dto.CreatedDate,
                ExpirationDate = dto.ExpirationDate,
                UserFk = dto.UserFk,
                PasswordStatus = dto.PasswordStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.Passwords.Add(entity);
            await _context.SaveChangesAsync();

            dto.PasswordId = entity.PasswordId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, PasswordDto dto)
        {
            var entity = await _context.Passwords.FindAsync(id);
            if (entity == null) return false;

            entity.Password1 = dto.Password1;
            entity.CreatedDate = dto.CreatedDate;
            entity.ExpirationDate = dto.ExpirationDate;
            entity.UserFk = dto.UserFk;
            entity.PasswordStatus = dto.PasswordStatus;
            entity.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            entity.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Passwords.FindAsync(id);
            if (entity == null) return false;

            _context.Passwords.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
