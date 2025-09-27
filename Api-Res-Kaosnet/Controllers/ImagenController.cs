using Api_Res_Kaosnet.DTO;
using Api_Res_Kaosnet.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Api_Res_Kaosnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ImagenController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // POST: api/Imagen/Upload
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("No se seleccionó ningún archivo.");

            // Genera un nombre único para evitar sobreescritura
            var nombreArchivo = Guid.NewGuid() + Path.GetExtension(archivo.FileName);

            // Carpeta física wwwroot/imagenes
            var rutaCarpeta = Path.Combine(_env.WebRootPath, "imagenes");

            // Crear carpeta si no existe
            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            var rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);

            // Guardar archivo en wwwroot/imagenes
            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // Crear URL pública
            var imagenUrl = $"/imagenes/{nombreArchivo}";

            return Ok(new { ImagenUrl = imagenUrl });
        }
    }
}
