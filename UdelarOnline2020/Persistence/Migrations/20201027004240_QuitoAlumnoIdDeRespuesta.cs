using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class QuitoAlumnoIdDeRespuesta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Actividad_EncuestaActividadId",
                table: "Respuesta");

            migrationBuilder.DropIndex(
                name: "IX_Respuesta_EncuestaActividadId",
                table: "Respuesta");

            migrationBuilder.DropColumn(
                name: "EncuestaActividadId",
                table: "Respuesta");

            migrationBuilder.AddColumn<Guid>(
                name: "ActividadId",
                table: "Respuesta",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActividadId",
                table: "Respuesta");

            migrationBuilder.AddColumn<Guid>(
                name: "EncuestaActividadId",
                table: "Respuesta",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_EncuestaActividadId",
                table: "Respuesta",
                column: "EncuestaActividadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_Actividad_EncuestaActividadId",
                table: "Respuesta",
                column: "EncuestaActividadId",
                principalTable: "Actividad",
                principalColumn: "ActividadId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
