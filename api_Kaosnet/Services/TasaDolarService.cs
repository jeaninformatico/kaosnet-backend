using api_Kaosnet.Data;
using api_Kaosnet.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;

namespace api_Kaosnet.Services
{
    public class TasaDolarService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _http;

        public TasaDolarService(AppDbContext context)
        {
            _context = context;
            _http = new HttpClient();
        }

        public async Task<TasaDolar> ActualizarDesdeDolarApiAsync()
        {
            try
            {
                var response = await _http.GetAsync("https://ve.dolarapi.com/v1/dollar");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var tasa = doc.RootElement.GetProperty("price").GetDecimal();

                var nuevaTasa = new TasaDolar
                {
                    Tasa = tasa,
                    Fuente = "DolarApi.com",
                    Fecha = DateTime.Now
                };

                _context.TasasDolar.Add(nuevaTasa);
                await _context.SaveChangesAsync();

                return nuevaTasa;
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("No se pudo obtener la tasa del dólar: " + ex.Message);
            }
        }

        public async Task<TasaDolar?> ObtenerUltimaAsync()
        {
            return await _context.TasasDolar
                .OrderByDescending(t => t.Fecha)
                .FirstOrDefaultAsync();
        }
    }
}
