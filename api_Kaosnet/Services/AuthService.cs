using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Models;
using Microsoft.EntityFrameworkCore;

namespace api_Kaosnet.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> LoginAsync(LoginDto dto)
        {
            // Si estás usando hashing, reemplaza esta línea con tu algoritmo de hash
            var clave = dto.Clave;

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Correo == dto.Correo && u.ClaveHash == clave);

            return usuario;
        }
    }
}
