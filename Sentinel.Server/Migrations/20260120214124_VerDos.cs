using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sentinel.Server.Migrations
{
    /// <inheritdoc />
    public partial class VerDos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TablaRol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablaRol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TablaUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClaveHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ClaveSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablaUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TablaEvento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actualizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablaEvento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TablaEvento_TablaUsuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "TablaUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TablaUsuarioRol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    RolId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablaUsuarioRol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TablaUsuarioRol_TablaRol_RolId",
                        column: x => x.RolId,
                        principalTable: "TablaRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TablaUsuarioRol_TablaUsuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "TablaUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TablaEvento_UsuarioId",
                table: "TablaEvento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "UX_Correo",
                table: "TablaUsuario",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TablaUsuarioRol_RolId",
                table: "TablaUsuarioRol",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_TablaUsuarioRol_UsuarioId",
                table: "TablaUsuarioRol",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TablaEvento");

            migrationBuilder.DropTable(
                name: "TablaUsuarioRol");

            migrationBuilder.DropTable(
                name: "TablaRol");

            migrationBuilder.DropTable(
                name: "TablaUsuario");
        }
    }
}
