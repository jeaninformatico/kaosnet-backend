using Api_Kaos_Net.Data;
using Api_Kaos_Net.DTO;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Kaos_Net.Services
{
    public class RoleAccessService : IRoleAccessService
    {
        private readonly KaosnetDbContext _context;

        public RoleAccessService(KaosnetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleAccessDto>> GetAllAsync()
        {
            return await _context.RoleAccesses
                .Select(r => new RoleAccessDto
                {
                    RoleAccessId = r.RoleAccessId,
                    RoleFk = r.RoleFk,
                    ViewFk = r.ViewFk,
                    IsWrite = r.IsWrite == 1,
                    RoleAccessStatus = r.RoleAccessStatus,
                    IsActive = r.IsActive == 1,
                    Idsession = r.Idsession
                }).ToListAsync();
        }

        public async Task<RoleAccessDto?> GetByIdAsync(int id)
        {
            var r = await _context.RoleAccesses.FindAsync(id);
            if (r == null) return null;

            return new RoleAccessDto
            {
                RoleAccessId = r.RoleAccessId,
                RoleFk = r.RoleFk,
                ViewFk = r.ViewFk,
                IsWrite = r.IsWrite == 1,
                RoleAccessStatus = r.RoleAccessStatus,
                IsActive = r.IsActive == 1,
                Idsession = r.Idsession
            };
        }

        public async Task<RoleAccessDto> CreateAsync(RoleAccessDto dto)
        {
            var entity = new RoleAccess
            {
                RoleFk = dto.RoleFk,
                ViewFk = dto.ViewFk,
                IsWrite = (sbyte?)(dto.IsWrite == true ? 1 : 0),
                RoleAccessStatus = dto.RoleAccessStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.RoleAccesses.Add(entity);
            await _context.SaveChangesAsync();

            dto.RoleAccessId = entity.RoleAccessId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, RoleAccessDto dto)
        {
            var entity = await _context.RoleAccesses.FindAsync(id);
            if (entity == null) return false;

            entity.RoleFk = dto.RoleFk;
            entity.ViewFk = dto.ViewFk;
            entity.IsWrite = (sbyte?)(dto.IsWrite == true ? 1 : 0);
            entity.RoleAccessStatus = dto.RoleAccessStatus;
            entity.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            entity.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.RoleAccesses.FindAsync(id);
            if (entity == null) return false;

            _context.RoleAccesses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
