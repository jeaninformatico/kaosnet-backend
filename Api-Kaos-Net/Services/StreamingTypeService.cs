using Api_Kaos_Net.Data;
using Api_Kaos_Net.DTOs;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Kaos_Net.Services
{
    public class StreamingTypeService : IStreamingTypeService
    {
        private readonly KaosnetDbContext _context;

        public StreamingTypeService(KaosnetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StreamingTypeDto>> GetAllAsync()
        {
            return await _context.StreamingTypes
                .Select(t => new StreamingTypeDto
                {
                    StreamingTypeId = t.StreamingTypeId,
                    StreamingTypeName = t.StreamingTypeName,
                    StreamingTypeDescription = t.StreamingTypeDescription,
                    StreamingTypeStatus = t.StreamingTypeStatus,
                    IsActive = t.IsActive == 1,
                    Idsession = t.Idsession
                }).ToListAsync();
        }

        public async Task<StreamingTypeDto?> GetByIdAsync(int id)
        {
            var t = await _context.StreamingTypes.FindAsync(id);
            if (t == null) return null;

            return new StreamingTypeDto
            {
                StreamingTypeId = t.StreamingTypeId,
                StreamingTypeName = t.StreamingTypeName,
                StreamingTypeDescription = t.StreamingTypeDescription,
                StreamingTypeStatus = t.StreamingTypeStatus,
                IsActive = t.IsActive == 1,
                Idsession = t.Idsession
            };
        }

        public async Task<StreamingTypeDto> CreateAsync(StreamingTypeDto dto)
        {
            var entity = new StreamingType
            {
                StreamingTypeName = dto.StreamingTypeName,
                StreamingTypeDescription = dto.StreamingTypeDescription,
                StreamingTypeStatus = dto.StreamingTypeStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.StreamingTypes.Add(entity);
            await _context.SaveChangesAsync();

            dto.StreamingTypeId = entity.StreamingTypeId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, StreamingTypeDto dto)
        {
            var entity = await _context.StreamingTypes.FindAsync(id);
            if (entity == null) return false;

            entity.StreamingTypeName = dto.StreamingTypeName;
            entity.StreamingTypeDescription = dto.StreamingTypeDescription;
            entity.StreamingTypeStatus = dto.StreamingTypeStatus;
            entity.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            entity.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.StreamingTypes.FindAsync(id);
            if (entity == null) return false;

            _context.StreamingTypes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
