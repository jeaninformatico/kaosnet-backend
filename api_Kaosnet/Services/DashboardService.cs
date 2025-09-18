using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using Microsoft.EntityFrameworkCore;

namespace api_Kaosnet.Services
{
    public class DashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> ObtenerAsync()
        {
            var dto = new DashboardDto
            {
                TotalUsuarios = await _context.Usuarios.CountAsync(),
                TotalCuentas = await _context.Cuentas.CountAsync(),
                TotalVentas = await _context.Ventas.SumAsync(v => v.Monto),
                RecuperacionesActivas = await _context.Recuperaciones.CountAsync(r => !r.Usado && r.Expiracion > DateTime.Now)
            };

            var tasa = await _context.TasasDolar
                .OrderByDescending(t => t.Fecha)
                .FirstOrDefaultAsync();

            dto.TasaDolarActual = tasa?.Tasa ?? 0;

            var configList = await _context.Configuraciones.ToListAsync();
            foreach (var c in configList)
            {
                dto.Configuraciones[c.Clave] = c.Valor;
            }

            return dto;
        }
    }
}
