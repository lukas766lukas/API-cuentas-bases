namespace Sentinel.Server.Modelos
{
    public class Rol
    {
        public int Id { get; set; }

        public string Tipo { get; set; } = null!;

        public ICollection<UsuarioRol> RolUser { get; set; } = new List<UsuarioRol>();
    }
}
//preguntaer despeus sobre icollector porque no se si esta bien 