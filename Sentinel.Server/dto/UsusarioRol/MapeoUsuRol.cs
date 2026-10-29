using Mapster;
using Sentinel.Server.dto.Usuario;
using Sentinel.Server.Modelos;
using System.Threading;

namespace Sentinel.Server.dto.UsusarioRol
{
    public class MapeoUsuRol : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //--------mapeo de dto a modelo para mostrar
            config.NewConfig<DtoUnionRolUser, Modelos.UsuarioRol>();
            //--------mapeo de modelo a dto para mostrar
            TypeAdapterConfig<Modelos.Rol, DtoReadRol>.NewConfig();


//no necesito usar el .map() porque tanto en modelo y dto tienen lo mismos nombres de propiedades solo cuando sea diferente o especial


        }

    }
}
