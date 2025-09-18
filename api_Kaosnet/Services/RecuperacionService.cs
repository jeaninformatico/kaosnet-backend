using System.Security.Cryptography;
using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Models;
using Microsoft.EntityFrameworkCore;

namespace api_Kaosnet.Services
{
    public class RecuperacionService
    {
        private readonly AppDbContext _context;

        public RecuperacionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> SolicitarAsync(string correo)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuario == null)
                throw new InvalidOperationException("Correo no registrado.");

            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

            var recuperacion = new Recuperacion
            {
                UsuarioId = usuario.Id,
                Token = token,
                Expiracion = DateTime.Now.AddMinutes(30),
                Usado = false
            };

            _context.Recuperaciones.Add(recuperacion);
            await _context.SaveChangesAsync();

            return token;
        }

        public async Task<bool> ConfirmarAsync(string token, string nuevaClave)
        {
            var registro = await _context.Recuperaciones
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(r => r.Token == token && !r.Usado);

            if (registro == null || registro.Expiracion < DateTime.Now)
                throw new InvalidOperationException("Token inválido o expirado.");

            registro.Usado = true;

            // Aplica hash si usas hashing
            var claveHash = HashHelper.Hash(nuevaClave);
            registro.Usuario!.ClaveHash = claveHash;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
