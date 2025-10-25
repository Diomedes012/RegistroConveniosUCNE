using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroConveniosUCNE.Migrations
{
    /// <inheritdoc />
    public partial class RutaArchivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ArchivoPrincipal",
                table: "Convenios",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 255,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ArchivoPrincipal",
                table: "Convenios",
                type: "TEXT",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
