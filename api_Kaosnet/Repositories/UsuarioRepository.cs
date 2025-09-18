using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Models;
using Microsoft.EntityFrameworkCore;

namespace api_Kaosnet.Repositories
{
    public class UsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetAllAsync() =>
            await _context.Usuarios.Include(u => u.Rol).ToListAsync();

        public async Task<Usuario?> GetByIdAsync(int id) =>
            await _context.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Id == id);

        public async Task<Usuario> CreateAsync(UsuarioDto dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                ClaveHash = dto.ClaveHash,
                ImagenUrl = dto.ImagenUrl,
                Telefono = dto.Telefono,
                Cedula = dto.Cedula,
                RolId = dto.RolId
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> UpdateAsync(int id, UsuarioDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            usuario.Nombre = dto.Nombre;
            usuario.Correo = dto.Correo;
            usuario.ClaveHash = dto.ClaveHash;
            usuario.ImagenUrl = dto.ImagenUrl;
            usuario.Telefono = dto.Telefono;
            usuario.Cedula = dto.Cedula;
            usuario.RolId = dto.RolId;
            usuario.ActualizadoEn = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
