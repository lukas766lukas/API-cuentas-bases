using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Sentinel.Server.dto.Evento;
using Sentinel.Server.dto.Usuario;
using Sentinel.Server.dto.UsusarioRol;
using Sentinel.Server.Modelos;
using Sentinel.Server.Servicios.Encriptar;
using Sentinel.Server.Servicios.JWT;
using Sentinel.Server.Servicios.Sesion;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Sentinel.Server.Servicios
{
    public class Service : IService
    {
        private readonly AppDbContext _context;
        private readonly IHashPassword _hashPassword;
        private readonly IJwtService _jwtServicio;
        private readonly ISessionService _sesionservice;
        public Service(AppDbContext context, IHashPassword hashPassword, IJwtService jwtServicio, ISessionService sessionService)
        {
            _context = context;
            _hashPassword = hashPassword;
            _jwtServicio = jwtServicio;
            _sesionservice=sessionService;
            

        }

        public async Task AsignarRol(DtoUnionRolUser dto)
        {
            //consultamos si existen esos id

            var rolexiste = await _context.DBCRols.FirstOrDefaultAsync(r => r.Id == dto.RolId);
            var usuarioexitente = await _context.DBCUsuarios.FirstOrDefaultAsync(u => u.Id == dto.UsuarioId);
            var existe = await _context.DBCUsuariosRols.AnyAsync(ur => ur.UsuarioId == dto.UsuarioId && ur.RolId == dto.RolId);

            if (existe)
                throw new InvalidOperationException("El usuario ya tiene este rol");
            if (rolexiste == null || usuarioexitente==null) {
                throw new InvalidOperationException("estos id no existen");
            }
            //  Crear relación
            var userRol = new UsuarioRol
            {
                UsuarioId = dto.UsuarioId,
                RolId = dto.RolId
            };
            _context.DBCUsuariosRols.Add(userRol);
            await _context.SaveChangesAsync();


        }

        public Task BuscarUsuario(DtoBuscarUser dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CorreoExisteAsync(string correo)
        {
            return await _context.DBCUsuarios.AnyAsync(u => u.Correo == correo);

        }

        public async Task CrearUsuario(DtoRegistrarUser dto)
        {
            // 1️ Validar correo para que no se repita en la BD.
            if (await CorreoExisteAsync(dto.Correo))
                throw new InvalidOperationException("El correo ya está registrado.");

            // 2️ Generar hash y salt
            _hashPassword.CrearClaveHash(dto.Password, out byte[] hash, out byte[] salt);
            // 3 crear un usuario atravez del mapeo
            var usuario= dto.Adapt<Usuario>(TypeAdapterConfig.GlobalSettings);
            // 4️ Asignar manualmente los campos que no vienen del DTO
            usuario.ClaveHash = hash;
            usuario.ClaveSalt = salt;

            // 5️ Guardar en la base de datos
            _context.DBCUsuarios.Add(usuario);
            await _context.SaveChangesAsync();

        }

        

        public async Task<DtoLoginResponse> LoginAsync(DtoLogin dto)
        {
            // 🔹 Paso 2: Buscar usuario
            var usuario = await _context.DBCUsuarios
            .Include(u => u.UserRol)
            .ThenInclude(ur => ur.Rol)
            .FirstOrDefaultAsync(u => u.Correo == dto.Correo);
            
            if (usuario == null)
                throw new Exception("Usuario no encontrado");
            usuario.IsActive = true;
            if (!usuario.IsActive)
                throw new Exception("Cuenta deshabilitada");

            // 🔹 Paso 3: Verificar contraseña
            bool claveValida = _hashPassword.VerificarClaveHash(
                dto.Password,
                usuario.ClaveHash,
                usuario.ClaveSalt
            );

            if (!claveValida)
                throw new Exception("Credenciales inválidas");



            var usuarioDto = usuario.Adapt<DtoReadUsuario>();//adaptamos a usuario

           // 🔹 Obtener roles
            var roles = usuario.UserRol
                .Select(ur => ur.Rol.Tipo)
                .ToList();
            //generamos los tokens

            var token = _jwtServicio.GenerarToken(usuarioDto, roles);

            //retornamos nuestro constructor
            return new DtoLoginResponse
            {
                AccessToken = token,
                User = usuarioDto
            };
           
        }

        

        public Task<bool> ValidarCampos(string correo, string clave)

        {
            







            //var usuario = await _context.Usuarios
            //.FirstOrDefaultAsync(u => u.Correo == correo);

            //if (usuario == null)
            //    return null;

            //// ⚠️ Ejemplo simple (en producción usa hash)
            //if (usuario.Password != password)
            //    return null;

            //return new DtoReadUsuario
            //{
            //    Id = usuario.Id,
            //    Correo = usuario.Correo,
            //    Nombre = usuario.Nombre
            //};

            throw new NotImplementedException();
        }

        public async Task ActualizarEvento(int id, DtoUpdateEvento dto)
        {
            byte[] archivoBytes;

            // Buscar el evento por ID
            var idevento = await _context.DBCEventos.FindAsync(id);
            if (idevento == null)
            {
                // Si no se encuentra.
                 throw new Exception("no se encuentra ningun registro con ese id");
            }
            //lo encuentra y actualiza
            // Mapear el evento (entidad) a un DtoReadEvento utilizando Mapster

            
            //convertimos la imagen en bytes
            using (var ms = new MemoryStream())
            {
                await dto.ImagenFile!.CopyToAsync(ms);
                archivoBytes = ms.ToArray();
            }

            idevento.Descripcion = dto.Descripcion!;
            idevento.Imagen = archivoBytes;
            await _context.SaveChangesAsync();


        }
        
        public async Task CrearEvento(DtoaddEvento evento)
        {
            var newevento = evento.Adapt<Evento>(TypeAdapterConfig.GlobalSettings);
            // 1 Guardar en la base de datos
            _context.DBCEventos.Add(newevento);
            await _context.SaveChangesAsync();


            
        }
    }
}
