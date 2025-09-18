using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Models;
using Microsoft.EntityFrameworkCore;

namespace api_Kaosnet.Repositories
{
    public class CuentaRepository
    {
        private readonly AppDbContext _context;

        public CuentaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cuenta> CreateAsync(CuentaDto dto)
        {
            var cuenta = new Cuenta
            {
                UsuarioId = dto.UsuarioId,
                Tipo = dto.Tipo,
                Saldo = dto.Saldo,
                Estado = "activa",
                CreadoEn = DateTime.Now
            };

            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();

            return cuenta;
        }

        public async Task<List<Cuenta>> GetByUsuarioAsync(int usuarioId)
        {
            return await _context.Cuentas
                .Where(c => c.UsuarioId == usuarioId)
                .Include(c => c.Usuario)
                .ToListAsync();
        }
    }
}
