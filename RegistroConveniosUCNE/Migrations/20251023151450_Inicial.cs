using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroConveniosUCNE.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instituciones",
                columns: table => new
                {
                    IdInstitucion = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Telefono = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    TipoInstitucion = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituciones", x => x.IdInstitucion);
                });

            migrationBuilder.CreateTable(
                name: "Responsables",
                columns: table => new
                {
                    IdResponsable = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Cargo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Departamento = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsables", x => x.IdResponsable);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreRol = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    UsuarioLogin = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ContrasenaHash = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    EmailInstitucional = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdRol = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Convenios",
                columns: table => new
                {
                    IdConvenio = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DescripcionObjetivos = table.Column<string>(type: "TEXT", nullable: true),
                    TipoConvenio = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Categoria = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    FechaFirma = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DuracionMeses = table.Column<int>(type: "INTEGER", nullable: true),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ArchivoPrincipal = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreadoPor = table.Column<int>(type: "INTEGER", nullable: true),
                    UltimaModificacion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Convenios", x => x.IdConvenio);
                    table.ForeignKey(
                        name: "FK_Convenios_Usuarios_CreadoPor",
                        column: x => x.CreadoPor,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Actividades",
                columns: table => new
                {
                    IdActividad = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdConvenio = table.Column<int>(type: "INTEGER", nullable: false),
                    TituloActividad = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    FechaActividad = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EvidenciaArchivo = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    CreadoPor = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividades", x => x.IdActividad);
                    table.ForeignKey(
                        name: "FK_Actividades_Convenios_IdConvenio",
                        column: x => x.IdConvenio,
                        principalTable: "Convenios",
                        principalColumn: "IdConvenio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actividades_Usuarios_CreadoPor",
                        column: x => x.CreadoPor,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    IdAlerta = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdConvenio = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaGenerada = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DiasAnticipacion = table.Column<int>(type: "INTEGER", nullable: false),
                    EstadoAlerta = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Destinatarios = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.IdAlerta);
                    table.ForeignKey(
                        name: "FK_Alertas_Convenios_IdConvenio",
                        column: x => x.IdConvenio,
                        principalTable: "Convenios",
                        principalColumn: "IdConvenio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConvenioInstituciones",
                columns: table => new
                {
                    ConvenioInstitucionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdConvenio = table.Column<int>(type: "INTEGER", nullable: false),
                    IdInstitucion = table.Column<int>(type: "INTEGER", nullable: false),
                    Rol = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConvenioInstituciones", x => x.ConvenioInstitucionId);
                    table.ForeignKey(
                        name: "FK_ConvenioInstituciones_Convenios_IdConvenio",
                        column: x => x.IdConvenio,
                        principalTable: "Convenios",
                        principalColumn: "IdConvenio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConvenioInstituciones_Instituciones_IdInstitucion",
                        column: x => x.IdInstitucion,
                        principalTable: "Instituciones",
                        principalColumn: "IdInstitucion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConvenioResponsables",
                columns: table => new
                {
                    ConvenioResponsableId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdConvenio = table.Column<int>(type: "INTEGER", nullable: false),
                    IdResponsable = table.Column<int>(type: "INTEGER", nullable: false),
                    Rol = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConvenioResponsables", x => x.ConvenioResponsableId);
                    table.ForeignKey(
                        name: "FK_ConvenioResponsables_Convenios_IdConvenio",
                        column: x => x.IdConvenio,
                        principalTable: "Convenios",
                        principalColumn: "IdConvenio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConvenioResponsables_Responsables_IdResponsable",
                        column: x => x.IdResponsable,
                        principalTable: "Responsables",
                        principalColumn: "IdResponsable",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_CreadoPor",
                table: "Actividades",
                column: "CreadoPor");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_IdConvenio",
                table: "Actividades",
                column: "IdConvenio");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_IdConvenio",
                table: "Alertas",
                column: "IdConvenio");

            migrationBuilder.CreateIndex(
                name: "IX_ConvenioInstituciones_IdConvenio",
                table: "ConvenioInstituciones",
                column: "IdConvenio");

            migrationBuilder.CreateIndex(
                name: "IX_ConvenioInstituciones_IdInstitucion",
                table: "ConvenioInstituciones",
                column: "IdInstitucion");

            migrationBuilder.CreateIndex(
                name: "IX_ConvenioResponsables_IdConvenio",
                table: "ConvenioResponsables",
                column: "IdConvenio");

            migrationBuilder.CreateIndex(
                name: "IX_ConvenioResponsables_IdResponsable",
                table: "ConvenioResponsables",
                column: "IdResponsable");

            migrationBuilder.CreateIndex(
                name: "IX_Convenios_CreadoPor",
                table: "Convenios",
                column: "CreadoPor");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actividades");

            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "ConvenioInstituciones");

            migrationBuilder.DropTable(
                name: "ConvenioResponsables");

            migrationBuilder.DropTable(
                name: "Instituciones");

            migrationBuilder.DropTable(
                name: "Convenios");

            migrationBuilder.DropTable(
                name: "Responsables");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
