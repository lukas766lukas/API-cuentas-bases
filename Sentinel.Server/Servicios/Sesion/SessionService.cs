using Sentinel.Server.Modelos;

namespace Sentinel.Server.Servicios.Sesion
{
    public class SessionService : ISessionService
    {
        private readonly Dictionary<Guid, UserSession> _sesiones = new();
        public void CerrarSesion(Guid sessionId)
        {
            _sesiones.Remove(sessionId);
        }

        public UserSession CrearSesion(int usuarioId)
        {
            //creamos la sesion usando un contructor
            var session = new UserSession
            {
                SessionId = Guid.NewGuid(),
                UsuarioId = usuarioId,
                Expira = DateTime.UtcNow.AddHours(1)
            };

            _sesiones[session.SessionId] = session;
            return session;
        }

        public bool SesionValida(Guid sessionId)
        {
            if (!_sesiones.TryGetValue(sessionId, out var sesion))
                return false;

            if (sesion.Expira < DateTime.UtcNow)
            {
                _sesiones.Remove(sessionId);
                return false;
            }

            return true;
        }
    }
}
