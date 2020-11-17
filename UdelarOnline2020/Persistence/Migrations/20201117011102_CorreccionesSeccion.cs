using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class CorreccionesSeccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TemplateCursoSeccionId",
                table: "TemplateCursoSeccion",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CursoSeccionId",
                table: "CursoSeccion",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateCursoSeccionId",
                table: "TemplateCursoSeccion");

            migrationBuilder.DropColumn(
                name: "CursoSeccionId",
                table: "CursoSeccion");
        }
    }
}
