namespace Sentinel.Server.Modelos
{
    public class UserSession
    {
        //modelo interno no necesita estar en la DB, hasta que se requiera optimizar a usar sesiones para desplegar bases de datos
        public Guid SessionId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Expira { get; set; }
    }
}
