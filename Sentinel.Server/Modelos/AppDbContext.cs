using Microsoft.EntityFrameworkCore;

namespace Sentinel.Server.Modelos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }//declaramos el constructor a travez de la herencia de la clase original 'base(options)' .
        // Aquí se registran TODOS los modelos:

        public DbSet<Usuario> DBCUsuarios { get; set; }    
        public DbSet<Rol>DBCRols { get; set; }
        public DbSet<UsuarioRol> DBCUsuariosRols { get;set; }
        public DbSet<Evento> DBCEventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            // ===== CONFIGURACIÓN DE Usuario PARA LA BASE DE DATOS =====
            modelBuilder.Entity<Usuario>(entity => {
                //nota** el nombre de las tablas no afecta a los DbSet<>

                // Nombre de la tabla para la base de datos(opcional)
                entity.ToTable("TablaUsuario");
                // Clave primaria
                entity.HasKey(u => u.Id);
                // -------PROPIEDADES CON RESTRICCIONES------
                entity.Property(u => u.Nombre)
                      .IsRequired()// campo es obligatorio
                       .HasMaxLength(100); 
                entity.Property(u => u.Apellido)
                      .IsRequired()
                      .HasMaxLength(100); 
                entity.Property(u => u.Correo)
                .IsRequired()
                .HasMaxLength(100);

                entity.HasIndex(u => u.Correo).IsUnique()
               .HasDatabaseName("UX_Correo");


            });

            // ===== CONFIGURACIÓN DE Rol PARA LA BASE DE DATOS =====
            modelBuilder.Entity<Rol>(entity => {
                // -------PROPIEDADES CON RESTRICCIONES------
                // Nombre de la tabla para la base de datos(opcional)
                entity.ToTable("TablaRol");
                // Clave primaria
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Tipo)
                .IsRequired()
                .HasMaxLength(10)
                ;
                

            });
            

            // ===== CONFIGURACIÓN DE UsuarioRol PARA LA BASE DE DATOS =====
            modelBuilder.Entity<UsuarioRol>(entity => {
                // Nombre de la tabla para la base de datos(opcional)
                entity.ToTable("TablaUsuarioRol");
                // Clave primaria
                entity.HasKey(ur => ur.Id);

                // -------PROPIEDADES CON RESTRICCIONES------
                // Relación 1:N en Pelicula y PeliculaGenero
                entity.HasOne(ur => ur.Usuario)
                     .WithMany(u => u.UserRol)
                     .HasForeignKey(ur => ur.UsuarioId)
                     .OnDelete(DeleteBehavior.SetNull); // o Restrict, cascade, según la lógica de negocio

                entity.HasOne(ur => ur.Rol)
                     .WithMany(r => r.RolUser)
                     .HasForeignKey(ur => ur.RolId)
                     .OnDelete(DeleteBehavior.SetNull); // o Restrict, cascade, según la lógica de negocio

            });


            // ===== CONFIGURACIÓN DE Evento PARA LA BASE DE DATOS =====
            modelBuilder.Entity<Evento>(entity => {
                // Nombre de la tabla para la base de datos(opcional)
                entity.ToTable("TablaEvento");
                // Clave primaria
                entity.HasKey(e => e.Id);

                // -------PROPIEDADES CON RESTRICCIONES------
                entity.Property(e => e.Imagen)
                .HasColumnType("varbinary(max)");
                //--------llaves foraneas--------
                // Relación 1:N en evento yUsuario  
                entity.HasOne(e => e.Usuario)
                     .WithMany(u => u.Eventos)
                     .HasForeignKey(e => e.UsuarioId)
                     .OnDelete(DeleteBehavior.SetNull); // o Restrict, cascade, según la lógica de negocio


            });

        }





    }
}
