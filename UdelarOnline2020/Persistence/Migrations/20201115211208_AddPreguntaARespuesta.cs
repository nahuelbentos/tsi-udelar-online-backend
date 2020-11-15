using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddPreguntaARespuesta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Actividad_EncuestaActividadId",
                table: "Respuesta");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Pregunta_PreguntaId",
                table: "Respuesta");

            migrationBuilder.DropIndex(
                name: "IX_Respuesta_EncuestaActividadId",
                table: "Respuesta");

            migrationBuilder.DropColumn(
                name: "EncuestaActividadId",
                table: "Respuesta");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreguntaId",
                table: "Respuesta",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_Pregunta_PreguntaId",
                table: "Respuesta",
                column: "PreguntaId",
                principalTable: "Pregunta",
                principalColumn: "PreguntaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Pregunta_PreguntaId",
                table: "Respuesta");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreguntaId",
                table: "Respuesta",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_Pregunta_PreguntaId",
                table: "Respuesta",
                column: "PreguntaId",
                principalTable: "Pregunta",
                principalColumn: "PreguntaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
