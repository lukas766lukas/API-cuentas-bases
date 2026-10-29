using System.ComponentModel.DataAnnotations;

namespace Sentinel.Server.Modelos
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;

        //seguridad de contraseñas
        public byte[] ClaveHash { get; set; } = null!;
        public byte[] ClaveSalt { get; set; } = null!;
        public bool IsActive { get; set; }  // cuenta habilitada

        //propiedades de navegacion

        public ICollection<UsuarioRol> UserRol { get; set; }=new List<UsuarioRol>();

        public ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    }
}
