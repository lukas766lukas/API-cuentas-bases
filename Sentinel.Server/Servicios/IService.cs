using Sentinel.Server.dto.Evento;
using Sentinel.Server.dto.Usuario;
using Sentinel.Server.dto.UsusarioRol;

namespace Sentinel.Server.Servicios
{
    public interface IService
    {
        Task<bool> CorreoExisteAsync(string correo);

        Task CrearUsuario(DtoRegistrarUser dto);
        Task BuscarUsuario(DtoBuscarUser dto);

       
        Task<DtoLoginResponse> LoginAsync(DtoLogin request);
       
        //crea un validador de correo y contraseña
        Task<bool> ValidarCampos(string correo, string clave);

        
        Task AsignarRol(DtoUnionRolUser dto);

        //------crear eventos---------
        Task CrearEvento(DtoaddEvento evento);

        //actualizar evento
        Task ActualizarEvento(int id, DtoUpdateEvento dto);
        //buscador de usuarios por id de rol.






    }
}
