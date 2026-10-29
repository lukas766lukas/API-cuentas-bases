namespace Sentinel.Server.dto.Evento
{
    public class DtoReadEvento
    {
        public int Id { get; set; }

        public string Descripcion { get; set; } = null!;

        public byte[]? Imagen { get; set; }

        //public DateTime Creacion { get; set; } = DateTime.UtcNow;
        public DateTime Actualizacion { get; set; } = DateTime.UtcNow;

        public int? UsuarioId { get; set; }   // // FK nullable
    }
}
