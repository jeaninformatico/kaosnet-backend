using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Models;
using Microsoft.EntityFrameworkCore;

namespace api_Kaosnet.Repositories
{
    public class VentaRepository
    {
        private readonly AppDbContext _context;

        public VentaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Venta> CreateAsync(VentaDto dto)
        {
            var cuenta = await _context.Cuentas.FindAsync(dto.CuentaId);
            if (cuenta == null)
                throw new InvalidOperationException("La cuenta no existe.");

            var venta = new Venta
            {
                CuentaId = dto.CuentaId,
                Monto = dto.Monto,
                Descripcion = dto.Descripcion,
                Fecha = DateTime.Now
            };

            cuenta.Saldo += dto.Monto; // Actualiza el saldo de la cuenta
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            return venta;
        }

        public async Task<List<Venta>> GetByCuentaAsync(int cuentaId)
        {
            return await _context.Ventas
                .Where(v => v.CuentaId == cuentaId)
                .Include(v => v.Cuenta)
                .ToListAsync();
        }
    }
}
