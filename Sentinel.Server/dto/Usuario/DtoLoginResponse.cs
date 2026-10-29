namespace Sentinel.Server.dto.Usuario
{
    public class DtoLoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;   // Token que se usa en cada request
        public required DtoReadUsuario User { get; set; } // Información básica del usuario
        //public Guid SessionId { get; set; }       // En JWT stateless puro NO es necesario.
    }
}
