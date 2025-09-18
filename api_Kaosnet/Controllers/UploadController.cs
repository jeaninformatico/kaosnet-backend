using Microsoft.AspNetCore.Mvc;

namespace api_Kaosnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        [HttpPost("imagen")]
        public async Task<IActionResult> SubirImagen(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("No se recibió ningún archivo.");

            var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
            var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            var url = $"{Request.Scheme}://{Request.Host}/uploads/{nombreArchivo}";
            return Ok(new { url });
        }
    }
}
