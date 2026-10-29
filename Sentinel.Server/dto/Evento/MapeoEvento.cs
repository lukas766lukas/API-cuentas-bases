using Mapster;
using Sentinel.Server.dto.Evento;
using Sentinel.Server.dto.Usuario;
using Sentinel.Server.dto.UsusarioRol;
using Sentinel.Server.Modelos;
namespace Sentinel.Server.dto.Evento
{
    public class MapeoEvento : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //--------Mapeo de DTO a Modelo para agregar(cuando agregamos o creamos)
            TypeAdapterConfig<DtoaddEvento, Modelos.Evento>
            .NewConfig()
            .Map(dest => dest.Creacion, src => DateTime.UtcNow) // Fecha de creación
            .Map(dest => dest.Actualizacion, src => DateTime.UtcNow); // Fecha de actualización inicial
            //.Map(des => des.Creacion, src=> DateTime.UtcNow)
            //.Map(de => de.Actualizacion, src => DateTime.UtcNow);

            //--------Mapeo de DTO a Modelo para actualizar
            TypeAdapterConfig<DtoUpdateEvento, Modelos.Evento>
            .NewConfig()
                .Map(dest => dest.Actualizacion, src => DateTime.UtcNow); // Actualización de la fecha

            //.Map(de => de.Actualizacion, src => DateTime.UtcNow);

            //--------mapeo de modelo a dto para mostrar

            TypeAdapterConfig<Modelos.Evento, DtoReadEvento>.NewConfig()
                .Map(dest => dest.Actualizacion, src => src.Actualizacion);  // Mapear 




        }


    }
}
