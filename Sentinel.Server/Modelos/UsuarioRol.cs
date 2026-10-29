using System.ComponentModel.DataAnnotations;

namespace Sentinel.Server.Modelos
{
    public class UsuarioRol
    {
        [Key]
        public int Id { get; set; }

        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public int? RolId { get; set; }
        public Rol Rol { get; set; } = null!;



    }
}
