using Api_Res_Kaosnet.DTO;
using Api_Res_Kaosnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Res_Kaosnet.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly KaosnetOriginalContext _context;

        public RolesController(KaosnetOriginalContext context)
        {
            _context = context;
        }

        // ✅ [GET] api/roles/lista
        [HttpGet("lista")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            var roles = await _context.Roles
                .Select(r => new RoleDto { IdRol = r.IdRol, Nombre = r.Nombre })
                .ToListAsync();

            return Ok(roles);
        }

        // 🔍 [GET] api/roles/detalle/{id}
        [HttpGet("detalle/{id}")]
        public async Task<ActionResult<RoleDto>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
                return NotFound(new { mensaje = $"No se encontró ningún rol con el ID {id}." });

            return Ok(new
            {
               
                rol = new RoleDto { IdRol = role.IdRol, Nombre = role.Nombre }
            });
        }

        // 🆕 [POST] api/roles/crear
        [HttpPost("crear")]
        public async Task<ActionResult<RoleDto>> CreateRole([FromBody] RoleCreateEditDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest(new { mensaje = "El nombre del rol es obligatorio." });

            var nombreExiste = await _context.Roles
                .AnyAsync(r => r.Nombre.ToLower() == dto.Nombre.ToLower());

            if (nombreExiste)
                return BadRequest(new { mensaje = "Ya existe un rol con ese nombre. Por favor elige uno diferente." });

            var nuevoRol = new Role { Nombre = dto.Nombre };
            _context.Roles.Add(nuevoRol);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRole), new { id = nuevoRol.IdRol },
                new
                {
                    mensaje = $"Rol creado exitosamente: {dto.Nombre}",
                   
                    rol = new RoleDto { IdRol = nuevoRol.IdRol, Nombre = nuevoRol.Nombre }
                });
        }

        // ✏️ [PUT] api/roles/editar
        [HttpPut("editar")]
        public async Task<IActionResult> UpdateRole([FromBody] RoleUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest(new { mensaje = "El nombre del rol es obligatorio." });

            var role = await _context.Roles.FindAsync(dto.IdRol);
            if (role == null)
                return NotFound(new { mensaje = $"No se encontró ningún rol con el ID {dto.IdRol}." });

            var nombreExiste = await _context.Roles
                .AnyAsync(r => r.Nombre.ToLower() == dto.Nombre.ToLower() && r.IdRol != dto.IdRol);

            if (nombreExiste)
                return BadRequest(new { mensaje = "Ya existe otro rol con ese nombre. Por favor elige uno diferente." });

            role.Nombre = dto.Nombre;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = $"Rol actualizado correctamente a: {dto.Nombre}",
               
            });
        }

        // 🗑️ [DELETE] api/roles/eliminar/{id}
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return NotFound(new { mensaje = $"No se encontró ningún rol con el ID {id}." });

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = $"Rol eliminado: {role.Nombre}",
               
            });
        }
    }
}
