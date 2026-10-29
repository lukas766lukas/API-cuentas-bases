using Sentinel.Server.dto.Usuario;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentinel.Server.dto.Evento
{
    public class DtoaddEvento

    {
        public string Descripcion { get; set; } = string.Empty;

        public IFormFile? ImagenFile { get; set; } // solo frontend
        public int UsuarioId { get; set; } // solo enviamos el Id
    }
}
