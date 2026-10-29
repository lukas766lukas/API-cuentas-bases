namespace Sentinel.Server.dto.Usuario
{
    public class DtoRegistrarUser
    {
        public  string Nombre { get; set; } = null!;
        public  string Apellido { get; set; } = null!;
        public  string Correo { get; set; } = null!;
        public  string Password { get; set; } = null!;
        public  string Password_confirmar { get; set; } = null!;
    }
}
