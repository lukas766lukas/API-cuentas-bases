using Sentinel.Server.Modelos;

namespace Sentinel.Server.Servicios.Sesion
{
    public interface ISessionService
    {

        UserSession CrearSesion(int usuarioId);
        bool SesionValida(Guid sessionId);
        void CerrarSesion(Guid sessionId);
    }
}
