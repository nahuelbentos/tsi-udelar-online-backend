using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddDominioMail : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "DominioMail",
                table: "Facultad",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DominioMail",
                table: "Facultad");

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
