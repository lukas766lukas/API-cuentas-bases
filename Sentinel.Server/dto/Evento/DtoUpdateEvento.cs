namespace Sentinel.Server.dto.Evento
{
    public class DtoUpdateEvento
    {
        public string? Descripcion { get; set; }
        //public byte[]? Imagen { get; set; }
        public IFormFile? ImagenFile { get; set; } // solo frontend
        //public DateTime Actualizacion { get; set; } = DateTime.UtcNow;

    }
}
