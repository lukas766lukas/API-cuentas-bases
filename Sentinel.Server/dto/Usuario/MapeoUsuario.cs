using Mapster;
using Sentinel.Server.dto.Usuario;
using Sentinel.Server.Modelos;
namespace Sentinel.Server.dto.Usuario
{
    public class MapeoUsuario : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Solo usas .Map() cuando los nombres NO coinciden o cuando necesitas transformar el valor
            config.NewConfig<DtoRegistrarUser, Modelos.Usuario>()
            
            .Ignore(dest => dest.ClaveHash)
            .Ignore(dest => dest.ClaveSalt)
            .Ignore(dest => dest.UserRol)
            .Ignore(dest => dest.Eventos)
            .Ignore(dest => dest.Id);



            //mapeo de modelo a dto para mostrar

            TypeAdapterConfig<Modelos.Usuario, DtoReadUsuario>.NewConfig();


            //config.NewConfig<Modelos.Usuario, DtoReadUsuario>();



        }

    }
}
