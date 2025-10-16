using Api_Kaos_Net.DTO;
using Api_Kaos_Net.Interfaces;
using Microsoft.EntityFrameworkCore;
using KaosNetApi.Data;
using KaosNetApi.Models;

namespace Api_Kaos_Net.Services
{
    public class RoleService : IRoleService
    {
        private readonly KaosNetDbContext _context;

        public RoleService(KaosNetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            return await _context.Roles
                .Select(r => new RoleDto
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName,
                    RoleDescription = r.RoleDescription,
                    RoleStatus = r.RoleStatus,
                    IsActive = r.IsActive == 1,
                    Idsession = r.Idsession
                }).ToListAsync();
        }

        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            var r = await _context.Roles.FindAsync(id);
            if (r == null) return null;

            return new RoleDto
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
                RoleDescription = r.RoleDescription,
                RoleStatus = r.RoleStatus,
                IsActive = r.IsActive == 1,
                Idsession = r.Idsession
            };
        }

        public async Task<RoleDto> CreateAsync(RoleCreateDto dto)
        {
            var role = new Role
            {
                RoleName = dto.RoleName,
                RoleDescription = dto.RoleDescription,
                RoleStatus = dto.RoleStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return new RoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                RoleDescription = role.RoleDescription,
                RoleStatus = role.RoleStatus,
                IsActive = role.IsActive == 1,
                Idsession = role.Idsession
            };
        }

        public async Task<bool> UpdateAsync(int id, RoleDto dto)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return false;

            role.RoleName = dto.RoleName;
            role.RoleDescription = dto.RoleDescription;
            role.RoleStatus = dto.RoleStatus;
            role.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            role.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return false;

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
