using Sentinel.Server.dto.Usuario;

namespace Sentinel.Server.Servicios.JWT
{
    public interface IJwtService
    {
        //Task<DtoLoginResponse> GenerarToken(DtoReadUsuario usuario);
        string GenerarToken(DtoReadUsuario usuario, List<string>? roles = null);
    }
}
