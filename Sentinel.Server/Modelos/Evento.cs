using System.ComponentModel.DataAnnotations.Schema;

namespace Sentinel.Server.Modelos
{
    public class Evento
    {
        public int Id { get; set; }

        public string Descripcion { get; set; } = null!;

        public byte[]? Imagen { get; set; }
        
        public DateTime Creacion { get; set; }= DateTime.UtcNow;
        public DateTime Actualizacion { get; set; }= DateTime.UtcNow;

        public int? UsuarioId { get; set; }   // // FK nullable
        public Usuario Usuario { get; set; } = null!;

    }
}
