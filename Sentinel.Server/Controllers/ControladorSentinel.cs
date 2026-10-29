using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sentinel.Server.dto.Evento;
using Sentinel.Server.dto.Usuario;
using Sentinel.Server.dto.UsusarioRol;
using Sentinel.Server.Modelos;
using Sentinel.Server.Servicios;
using System.Security.Claims;
namespace Sentinel.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorSentinel : ControllerBase
    {
        private readonly IService _service;
        private readonly AppDbContext _context;

        public ControladorSentinel(IService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }
        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<Usuario>>> ListarUsuario() { 
            var usuarios= await _context.DBCUsuarios.ToListAsync();
            return Ok(usuarios);
        }

        // REGISTRAR
        [HttpPost("registrar")]
        public async Task<ActionResult> RegistrarUsuario(DtoRegistrarUser dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                //llamamos a nuestra funcion de service para crear usuarios
                await _service.CrearUsuario(dto);
                return StatusCode(201, new
                {
                    message = "Usuario creado correctamente"
                });

            }
            catch (InvalidOperationException ex )// correo duplicado u otros de negocio
            {
                return Conflict(new { message = ex.Message }); // 409
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // 400
            }

        }

        //  LOGIN

        [HttpPost("login")]
        public async Task<ActionResult<DtoLoginResponse>> Login(DtoLogin dto)
        {
            try
            {
                var response = await _service.LoginAsync(dto);

                return Ok(response);   // ← Devuelve tu DtoLoginResponse
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
        // roles
        [HttpPost("addrol")]
        public async Task<IActionResult> CrearRol(Rol rol)
        {
            await _context.DBCRols.AddAsync(rol);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("unionroles")]
        public async Task<ActionResult> UnioRoles(DtoUnionRolUser dto)
        {
            try
            {
                 await _service.AsignarRol(dto);
                return StatusCode(201, new
                {
                    message = "rol asignado a usuario"
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
        //Eventos
        [HttpPost("addevento")]
        public async Task<ActionResult> SubirEvento(DtoaddEvento evento)
        {
            try
            {
                await _service.CrearEvento(evento);
                return StatusCode(201, new
                {
                    message = "Evento agregado correctamente"
                });

            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
        [HttpPut("updateevento")]
        public async Task<IActionResult> UpdaterEvento(int id, DtoUpdateEvento dto)
        {
            try {
                await _service.ActualizarEvento(id,dto);

                return StatusCode(201, new
                {
                    message = "Evento actualizado correctamente"
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }

        }
        //eliminar evento
        [HttpDelete("DeleteEvent/{id}")]
        public async Task<IActionResult> EliminarEvento(int id)
        {

            var borrarevento = await _context.DBCEventos.FindAsync(id);
            _context.DBCEventos.Remove(borrarevento!);
            await _context.SaveChangesAsync();
            return Ok();
        }



        [Authorize]
        [HttpGet]
        public IActionResult Test()
        {
            //aquí van los claims para ser leidos sobre todo los personalizados
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var nombre = User.FindFirst(ClaimTypes.Name)?.Value;

            
            return Ok(new
            {
                UserId = userId,
                Email = email,
                Nombre = nombre
            });
        }
        [Authorize]
        [HttpGet("me")]
        public IActionResult Perfil()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var correo = User.FindFirst("Correo")?.Value;
            var nombre = User.FindFirst(ClaimTypes.Name)?.Value;

            return Ok(new
            {
                Id = userId,
                Correo = correo,
                Nombre = nombre
            });
        }


        

    }
}
