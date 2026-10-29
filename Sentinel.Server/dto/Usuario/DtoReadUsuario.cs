namespace Sentinel.Server.dto.Usuario
{
    public class DtoReadUsuario
    {
        public int Id { get; set; }   // ✔ seguro
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
    }
}
