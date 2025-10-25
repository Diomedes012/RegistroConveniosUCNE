using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroConveniosUCNE.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_Convenios_IdConvenio",
                table: "Actividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_Usuarios_CreadoPor",
                table: "Actividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Alertas_Convenios_IdConvenio",
                table: "Alertas");

            migrationBuilder.DropForeignKey(
                name: "FK_ConvenioInstituciones_Convenios_IdConvenio",
                table: "ConvenioInstituciones");

            migrationBuilder.DropForeignKey(
                name: "FK_ConvenioInstituciones_Instituciones_IdInstitucion",
                table: "ConvenioInstituciones");

            migrationBuilder.DropForeignKey(
                name: "FK_ConvenioResponsables_Convenios_IdConvenio",
                table: "ConvenioResponsables");

            migrationBuilder.DropForeignKey(
                name: "FK_ConvenioResponsables_Responsables_IdResponsable",
                table: "ConvenioResponsables");

            migrationBuilder.DropForeignKey(
                name: "FK_Convenios_Usuarios_CreadoPor",
                table: "Convenios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_IdRol",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Responsables",
                table: "Responsables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instituciones",
                table: "Instituciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Convenios",
                table: "Convenios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConvenioResponsables",
                table: "ConvenioResponsables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConvenioInstituciones",
                table: "ConvenioInstituciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alertas",
                table: "Alertas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actividades",
                table: "Actividades");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Rol");

            migrationBuilder.RenameTable(
                name: "Responsables",
                newName: "Responsable");

            migrationBuilder.RenameTable(
                name: "Instituciones",
                newName: "Institucion");

            migrationBuilder.RenameTable(
                name: "Convenios",
                newName: "Convenio");

            migrationBuilder.RenameTable(
                name: "ConvenioResponsables",
                newName: "ConvenioResponsable");

            migrationBuilder.RenameTable(
                name: "ConvenioInstituciones",
                newName: "ConvenioInstitucion");

            migrationBuilder.RenameTable(
                name: "Alertas",
                newName: "Alerta");

            migrationBuilder.RenameTable(
                name: "Actividades",
                newName: "Actividad");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuario",
                newName: "IX_Usuario_IdRol");

            migrationBuilder.RenameIndex(
                name: "IX_Convenios_CreadoPor",
                table: "Convenio",
                newName: "IX_Convenio_CreadoPor");

            migrationBuilder.RenameIndex(
                name: "IX_ConvenioResponsables_IdResponsable",
                table: "ConvenioResponsable",
                newName: "IX_ConvenioResponsable_IdResponsable");

            migrationBuilder.RenameIndex(
                name: "IX_ConvenioResponsables_IdConvenio",
                table: "ConvenioResponsable",
                newName: "IX_ConvenioResponsable_IdConvenio");

            migrationBuilder.RenameIndex(
                name: "IX_ConvenioInstituciones_IdInstitucion",
                table: "ConvenioInstitucion",
                newName: "IX_ConvenioInstitucion_IdInstitucion");

            migrationBuilder.RenameIndex(
                name: "IX_ConvenioInstituciones_IdConvenio",
                table: "ConvenioInstitucion",
                newName: "IX_ConvenioInstitucion_IdConvenio");

            migrationBuilder.RenameIndex(
                name: "IX_Alertas_IdConvenio",
                table: "Alerta",
                newName: "IX_Alerta_IdConvenio");

            migrationBuilder.RenameIndex(
                name: "IX_Actividades_IdConvenio",
                table: "Actividad",
                newName: "IX_Actividad_IdConvenio");

            migrationBuilder.RenameIndex(
                name: "IX_Actividades_CreadoPor",
                table: "Actividad",
                newName: "IX_Actividad_CreadoPor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rol",
                table: "Rol",
                column: "IdRol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Responsable",
                table: "Responsable",
                column: "IdResponsable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Institucion",
                table: "Institucion",
                column: "IdInstitucion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Convenio",
                table: "Convenio",
                column: "IdConvenio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConvenioResponsable",
                table: "ConvenioResponsable",
                column: "ConvenioResponsableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConvenioInstitucion",
                table: "ConvenioInstitucion",
                column: "ConvenioInstitucionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alerta",
                table: "Alerta",
                column: "IdAlerta");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actividad",
                table: "Actividad",
                column: "IdActividad");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_Convenio_IdConvenio",
                table: "Actividad",
                column: "IdConvenio",
                principalTable: "Convenio",
                principalColumn: "IdConvenio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_Usuario_CreadoPor",
                table: "Actividad",
                column: "CreadoPor",
                principalTable: "Usuario",
                principalColumn: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerta_Convenio_IdConvenio",
                table: "Alerta",
                column: "IdConvenio",
                principalTable: "Convenio",
                principalColumn: "IdConvenio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Convenio_Usuario_CreadoPor",
                table: "Convenio",
                column: "CreadoPor",
                principalTable: "Usuario",
                principalColumn: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_ConvenioInstitucion_Convenio_IdConvenio",
                table: "ConvenioInstitucion",
                column: "IdConvenio",
                principalTable: "Convenio",
                principalColumn: "IdConvenio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConvenioInstitucion_Institucion_IdInstitucion",
                table: "ConvenioInstitucion",
                column: "IdInstitucion",
                principalTable: "Institucion",
                principalColumn: "IdInstitucion",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConvenioResponsable_Convenio_IdConvenio",
                table: "ConvenioResponsable",
                column: "IdConvenio",
                principalTable: "Convenio",
                principalColumn: "IdConvenio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConvenioResponsable_Responsable_IdResponsable",
                table: "ConvenioResponsable",
                column: "IdResponsable",
                principalTable: "Responsable",
                principalColumn: "IdResponsable",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Rol_IdRol",
                table: "Usuario",
                column: "IdRol",
                principalTable: "Rol",
                principalColumn: "IdRol",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Convenio_IdConvenio",
                table: "Actividad");

            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Usuario_CreadoPor",
                table: "Actividad");

            migrationBuilder.DropForeignKey(
                name: "FK_Alerta_Convenio_IdConvenio",
                table: "Alerta");

            migrationBuilder.DropForeignKey(
                name: "FK_Convenio_Usuario_CreadoPor",
                table: "Convenio");

            migrationBuilder.DropForeignKey(
                name: "FK_ConvenioInstitucion_Convenio_IdConvenio",
                table: "ConvenioInstitucion");

            migrationBuilder.DropForeignKey(
                name: "FK_ConvenioInstitucion_Institucion_IdInstitucion",
                table: "ConvenioInstitucion");

            migrationBuilder.DropForeignKey(
                name: "FK_ConvenioResponsable_Convenio_IdConvenio",
                table: "ConvenioResponsable");

            migrationBuilder.DropForeignKey(
                name: "FK_ConvenioResponsable_Responsable_IdResponsable",
                table: "ConvenioResponsable");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Rol_IdRol",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rol",
                table: "Rol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Responsable",
                table: "Responsable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Institucion",
                table: "Institucion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConvenioResponsable",
                table: "ConvenioResponsable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConvenioInstitucion",
                table: "ConvenioInstitucion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Convenio",
                table: "Convenio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alerta",
                table: "Alerta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actividad",
                table: "Actividad");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Rol",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Responsable",
                newName: "Responsables");

            migrationBuilder.RenameTable(
                name: "Institucion",
                newName: "Instituciones");

            migrationBuilder.RenameTable(
                name: "ConvenioResponsable",
                newName: "ConvenioResponsables");

            migrationBuilder.RenameTable(
                name: "ConvenioInstitucion",
                newName: "ConvenioInstituciones");

            migrationBuilder.RenameTable(
                name: "Convenio",
                newName: "Convenios");

            migrationBuilder.RenameTable(
                name: "Alerta",
                newName: "Alertas");

            migrationBuilder.RenameTable(
                name: "Actividad",
                newName: "Actividades");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_IdRol",
                table: "Usuarios",
                newName: "IX_Usuarios_IdRol");

            migrationBuilder.RenameIndex(
                name: "IX_ConvenioResponsable_IdResponsable",
                table: "ConvenioResponsables",
                newName: "IX_ConvenioResponsables_IdResponsable");

            migrationBuilder.RenameIndex(
                name: "IX_ConvenioResponsable_IdConvenio",
                table: "ConvenioResponsables",
                newName: "IX_ConvenioResponsables_IdConvenio");

            migrationBuilder.RenameIndex(
                name: "IX_ConvenioInstitucion_IdInstitucion",
                table: "ConvenioInstituciones",
                newName: "IX_ConvenioInstituciones_IdInstitucion");

            migrationBuilder.RenameIndex(
                name: "IX_ConvenioInstitucion_IdConvenio",
                table: "ConvenioInstituciones",
                newName: "IX_ConvenioInstituciones_IdConvenio");

            migrationBuilder.RenameIndex(
                name: "IX_Convenio_CreadoPor",
                table: "Convenios",
                newName: "IX_Convenios_CreadoPor");

            migrationBuilder.RenameIndex(
                name: "IX_Alerta_IdConvenio",
                table: "Alertas",
                newName: "IX_Alertas_IdConvenio");

            migrationBuilder.RenameIndex(
                name: "IX_Actividad_IdConvenio",
                table: "Actividades",
                newName: "IX_Actividades_IdConvenio");

            migrationBuilder.RenameIndex(
                name: "IX_Actividad_CreadoPor",
                table: "Actividades",
                newName: "IX_Actividades_CreadoPor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "IdRol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Responsables",
                table: "Responsables",
                column: "IdResponsable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instituciones",
                table: "Instituciones",
                column: "IdInstitucion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConvenioResponsables",
                table: "ConvenioResponsables",
                column: "ConvenioResponsableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConvenioInstituciones",
                table: "ConvenioInstituciones",
                column: "ConvenioInstitucionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Convenios",
                table: "Convenios",
                column: "IdConvenio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alertas",
                table: "Alertas",
                column: "IdAlerta");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actividades",
                table: "Actividades",
                column: "IdActividad");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_Convenios_IdConvenio",
                table: "Actividades",
                column: "IdConvenio",
                principalTable: "Convenios",
                principalColumn: "IdConvenio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_Usuarios_CreadoPor",
                table: "Actividades",
                column: "CreadoPor",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Alertas_Convenios_IdConvenio",
                table: "Alertas",
                column: "IdConvenio",
                principalTable: "Convenios",
                principalColumn: "IdConvenio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConvenioInstituciones_Convenios_IdConvenio",
                table: "ConvenioInstituciones",
                column: "IdConvenio",
                principalTable: "Convenios",
                principalColumn: "IdConvenio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConvenioInstituciones_Instituciones_IdInstitucion",
                table: "ConvenioInstituciones",
                column: "IdInstitucion",
                principalTable: "Instituciones",
                principalColumn: "IdInstitucion",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConvenioResponsables_Convenios_IdConvenio",
                table: "ConvenioResponsables",
                column: "IdConvenio",
                principalTable: "Convenios",
                principalColumn: "IdConvenio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConvenioResponsables_Responsables_IdResponsable",
                table: "ConvenioResponsables",
                column: "IdResponsable",
                principalTable: "Responsables",
                principalColumn: "IdResponsable",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Convenios_Usuarios_CreadoPor",
                table: "Convenios",
                column: "CreadoPor",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_IdRol",
                table: "Usuarios",
                column: "IdRol",
                principalTable: "Roles",
                principalColumn: "IdRol",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
