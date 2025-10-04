using Api_Kaos_Net.Data;
using Api_Kaos_Net.DTO;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Kaos_Net.Services
{
    public class ModuleService : IModuleService
    {
        private readonly KaosnetDbContext _context;

        public ModuleService(KaosnetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ModuleDto>> GetAllAsync()
        {
            return await _context.Modules
                .Select(m => new ModuleDto
                {
                    ModuleId = m.ModuleId,
                    ModuleName = m.ModuleName,
                    ModuleDescription = m.ModuleDescription,
                    ModuleIcon = m.ModuleIcon,
                    MenuSequence = m.MenuSequence,
                    ModuleStatus = m.ModuleStatus,
                    IsActive = m.IsActive == 1,
                    Idsession = m.Idsession
                }).ToListAsync();
        }

        public async Task<ModuleDto?> GetByIdAsync(int id)
        {
            var m = await _context.Modules.FindAsync(id);
            if (m == null) return null;

            return new ModuleDto
            {
                ModuleId = m.ModuleId,
                ModuleName = m.ModuleName,
                ModuleDescription = m.ModuleDescription,
                ModuleIcon = m.ModuleIcon,
                MenuSequence = m.MenuSequence,
                ModuleStatus = m.ModuleStatus,
                IsActive = m.IsActive == 1,
                Idsession = m.Idsession
            };
        }

        public async Task<ModuleDto> CreateAsync(ModuleDto dto)
        {
            var entity = new Module
            {
                ModuleName = dto.ModuleName,
                ModuleDescription = dto.ModuleDescription,
                ModuleIcon = dto.ModuleIcon,
                MenuSequence = dto.MenuSequence,
                ModuleStatus = dto.ModuleStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.Modules.Add(entity);
            await _context.SaveChangesAsync();

            dto.ModuleId = entity.ModuleId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, ModuleDto dto)
        {
            var entity = await _context.Modules.FindAsync(id);
            if (entity == null) return false;

            entity.ModuleName = dto.ModuleName;
            entity.ModuleDescription = dto.ModuleDescription;
            entity.ModuleIcon = dto.ModuleIcon;
            entity.MenuSequence = dto.MenuSequence;
            entity.ModuleStatus = dto.ModuleStatus;
            entity.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            entity.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Modules.FindAsync(id);
            if (entity == null) return false;

            _context.Modules.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
