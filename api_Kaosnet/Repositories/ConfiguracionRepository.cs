using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Models;
using Microsoft.EntityFrameworkCore;

namespace api_Kaosnet.Repositories
{
    public class ConfiguracionRepository
    {
        private readonly AppDbContext _context;

        public ConfiguracionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Configuracion> CrearOActualizarAsync(ConfiguracionDto dto)
        {
            var config = await _context.Configuraciones
                .FirstOrDefaultAsync(c => c.Clave == dto.Clave);

            if (config == null)
            {
                config = new Configuracion
                {
                    Clave = dto.Clave,
                    Valor = dto.Valor,
                    Tipo = dto.Tipo,
                    ActualizadoEn = DateTime.Now
                };
                _context.Configuraciones.Add(config);
            }
            else
            {
                config.Valor = dto.Valor;
                config.Tipo = dto.Tipo;
                config.ActualizadoEn = DateTime.Now;
                _context.Configuraciones.Update(config);
            }

            await _context.SaveChangesAsync();
            return config;
        }

        public async Task<Configuracion?> ObtenerPorClaveAsync(string clave)
        {
            return await _context.Configuraciones
                .FirstOrDefaultAsync(c => c.Clave == clave);
        }

        public async Task<List<Configuracion>> ListarTodasAsync()
        {
            return await _context.Configuraciones
                .OrderByDescending(c => c.ActualizadoEn)
                .ToListAsync();
        }
    }
}
