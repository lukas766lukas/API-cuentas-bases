using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sentinel.Server.dto.Usuario;
using Sentinel.Server.Modelos;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sentinel.Server.Servicios.JWT
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public JwtService(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context=context;
        }

        public string GenerarToken(DtoReadUsuario dtousuario, List<string>? roles = null)
        {
            
        //-------------definir claims, son como las credenciales de una tarjeta de identificacion
            var claims = new List<Claim>
            {
                // Claim estándar (ASP.NET lo entiende)
                new Claim(ClaimTypes.NameIdentifier, dtousuario.Id.ToString()),//es del DtoReadUsuario campo 'id'

   // Claims personalizados (tu dominio)| luego se modifican en Service.cs para que puedan ser leidos ya que ASP.net no los lee
                new Claim("NombreCompleto", $"{dtousuario.Nombre} {dtousuario.Apellido}"),
                new Claim("Correo", dtousuario.Correo),

                
            };
            // agregar roles si existen
            if (roles != null)
            {
                foreach (var rol in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));//añadimos un Claim con ese rol
                }
            }


            //-------------crear las firmas
            //importamos la llave del propio appsettings.json especificamente del 'JwtConfig'
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:Key"]!));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(//<-- con esto declaramos nuestro constructor
                issuer: _config["JwtConfig:Issuer"],//<--Quién emite el token Normalmente es tu API o servidor de autenticación.
                audience: _config["JwtConfig:Audience"],//<--Para quién está destinado el token
                claims: claims,//<-- llamamos a los claims que hemos creado
                expires: DateTime.UtcNow.AddHours(1),//<--definimos el tiempo de expiracion o Cuándo deja de ser válido el token
                signingCredentials: credentials//<--La firma del token que hemos creado con variables credentials y key
                );

            // aquí luego:
            // - clave secreta
            // - credenciales
            // - token
            // - return string

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
        
        
    
}
