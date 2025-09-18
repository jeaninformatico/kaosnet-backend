using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Models;

using api_Kaosnet.Data;
using api_Kaosnet.Models;
using Microsoft.EntityFrameworkCore;

namespace api_Kaosnet.Repositories
{
    public class RolRepository
    {
        private readonly AppDbContext _context;

        public RolRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> GetAllAsync() => await _context.Roles.ToListAsync();

        public async Task<Rol?> GetByIdAsync(int id) => await _context.Roles.FindAsync(id);

        public async Task<Rol> CreateAsync(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<bool> UpdateAsync(int id, RolDto dto)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null) return false;

            rol.Nombre = dto.Nombre;
            rol.Descripcion = dto.Descripcion;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null) return false;

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
