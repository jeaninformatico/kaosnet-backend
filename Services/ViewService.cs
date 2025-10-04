using Api_Kaos_Net.Data;
using Api_Kaos_Net.DTO;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Kaos_Net.Services
{
    public class ViewService : IViewService
    {
        private readonly KaosnetDbContext _context;

        public ViewService(KaosnetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ViewDto>> GetAllAsync()
        {
            return await _context.Views
                .Select(v => new ViewDto
                {
                    ViewId = v.ViewId,
                    ViewName = v.ViewName,
                    ViewDescription = v.ViewDescription,
                    ViewIcon = v.ViewIcon,
                    ModuleFk = v.ModuleFk,
                    ModuleSequence = v.ModuleSequence,
                    ViewPath = v.ViewPath,
                    ParentViewFk = v.ParentViewFk,
                    ViewStatus = v.ViewStatus,
                    IsActive = v.IsActive == 1,
                    Idsession = v.Idsession
                }).ToListAsync();
        }

        public async Task<ViewDto?> GetByIdAsync(int id)
        {
            var v = await _context.Views.FindAsync(id);
            if (v == null) return null;

            return new ViewDto
            {
                ViewId = v.ViewId,
                ViewName = v.ViewName,
                ViewDescription = v.ViewDescription,
                ViewIcon = v.ViewIcon,
                ModuleFk = v.ModuleFk,
                ModuleSequence = v.ModuleSequence,
                ViewPath = v.ViewPath,
                ParentViewFk = v.ParentViewFk,
                ViewStatus = v.ViewStatus,
                IsActive = v.IsActive == 1,
                Idsession = v.Idsession
            };
        }

        public async Task<ViewDto> CreateAsync(ViewDto dto)
        {
            var entity = new View
            {
                ViewName = dto.ViewName,
                ViewDescription = dto.ViewDescription,
                ViewIcon = dto.ViewIcon,
                ModuleFk = dto.ModuleFk,
                ModuleSequence = dto.ModuleSequence,
                ViewPath = dto.ViewPath,
                ParentViewFk = dto.ParentViewFk,
                ViewStatus = dto.ViewStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.Views.Add(entity);
            await _context.SaveChangesAsync();

            dto.ViewId = entity.ViewId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, ViewDto dto)
        {
            var entity = await _context.Views.FindAsync(id);
            if (entity == null) return false;

            entity.ViewName = dto.ViewName;
            entity.ViewDescription = dto.ViewDescription;
            entity.ViewIcon = dto.ViewIcon;
            entity.ModuleFk = dto.ModuleFk;
            entity.ModuleSequence = dto.ModuleSequence;
            entity.ViewPath = dto.ViewPath;
            entity.ParentViewFk = dto.ParentViewFk;
            entity.ViewStatus = dto.ViewStatus;
            entity.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            entity.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Views.FindAsync(id);
            if (entity == null) return false;

            _context.Views.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
