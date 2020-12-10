using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AgregoRealiadaPorAlumnoAAlumnoPruebaOnline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RealizadaPorAlumno",
                table: "AlumnoPruebaOnline",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealizadaPorAlumno",
                table: "AlumnoPruebaOnline");
        }
    }
}
