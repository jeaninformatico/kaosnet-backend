using Api_Res_Kaosnet.DTO;
using Api_Res_Kaosnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Res_Kaosnet.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly KaosnetOriginalContext _context;

        public ClientesController(KaosnetOriginalContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetClientes()
        {
            var clientes = await _context.Clientes
                .Select(c => new ClienteDto
                {
                    IdCliente = c.IdCliente,
                    Contacto = c.Contacto,
                    País = c.País
                })
                .ToListAsync();

            return Ok(clientes);
        }

        [HttpGet("detalle/{id}")]
        public async Task<ActionResult<ClienteDto>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound(new { mensaje = $"No se encontró ningún cliente con el ID {id}." });

            var dto = new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                Contacto = cliente.Contacto,
                País = cliente.País
            };

            return Ok(new { cliente = dto });
        }

        [HttpPost("crear")]
        public async Task<ActionResult<ClienteDto>> CreateCliente([FromBody] ClienteCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Contacto))
                return BadRequest(new { mensaje = "El contacto del cliente es obligatorio." });

            var existe = await _context.Clientes
                .AnyAsync(c => c.Contacto != null && c.Contacto.ToLower() == dto.Contacto.ToLower());

            if (existe)
                return BadRequest(new { mensaje = "Ya existe un cliente con ese contacto." });

            var nuevoCliente = new Cliente
            {
                Contacto = dto.Contacto,
                País = dto.País
            };

            _context.Clientes.Add(nuevoCliente);
            await _context.SaveChangesAsync();

            var result = new ClienteDto
            {
                IdCliente = nuevoCliente.IdCliente,
                Contacto = nuevoCliente.Contacto,
                País = nuevoCliente.País
            };

            return CreatedAtAction(nameof(GetCliente), new { id = nuevoCliente.IdCliente },
                new { mensaje = $"Cliente creado exitosamente: {dto.Contacto}", cliente = result });
        }

        [HttpPut("editar")]
        public async Task<IActionResult> UpdateCliente([FromBody] ClienteUpdateDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(dto.IdCliente);
            if (cliente == null)
                return NotFound(new { mensaje = $"No se encontró ningún cliente con el ID {dto.IdCliente}." });

            if (!string.IsNullOrWhiteSpace(dto.Contacto))
            {
                var existe = await _context.Clientes
                    .AnyAsync(c => c.Contacto != null && c.Contacto.ToLower() == dto.Contacto.ToLower() && c.IdCliente != dto.IdCliente);

                if (existe)
                    return BadRequest(new { mensaje = "Ya existe otro cliente con ese contacto." });
            }

            cliente.Contacto = dto.Contacto;
            cliente.País = dto.País;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"Cliente actualizado correctamente: {dto.Contacto}" });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound(new { mensaje = $"No se encontró ningún cliente con el ID {id}." });

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"Cliente eliminado: {cliente.Contacto}" });
        }
    }
}
