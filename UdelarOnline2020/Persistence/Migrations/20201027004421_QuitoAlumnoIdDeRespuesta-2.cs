using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class QuitoAlumnoIdDeRespuesta2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_AspNetUsers_AlumnoId1",
                table: "Respuesta");

            migrationBuilder.DropIndex(
                name: "IX_Respuesta_AlumnoId1",
                table: "Respuesta");

            migrationBuilder.DropColumn(
                name: "ActividadId",
                table: "Respuesta");

            migrationBuilder.DropColumn(
                name: "AlumnoId1",
                table: "Respuesta");

            migrationBuilder.AlterColumn<string>(
                name: "AlumnoId",
                table: "Respuesta",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "EncuestaActividadId",
                table: "Respuesta",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_AlumnoId",
                table: "Respuesta",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_EncuestaActividadId",
                table: "Respuesta",
                column: "EncuestaActividadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_AspNetUsers_AlumnoId",
                table: "Respuesta",
                column: "AlumnoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_Actividad_EncuestaActividadId",
                table: "Respuesta",
                column: "EncuestaActividadId",
                principalTable: "Actividad",
                principalColumn: "ActividadId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_AspNetUsers_AlumnoId",
                table: "Respuesta");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Actividad_EncuestaActividadId",
                table: "Respuesta");

            migrationBuilder.DropIndex(
                name: "IX_Respuesta_AlumnoId",
                table: "Respuesta");

            migrationBuilder.DropIndex(
                name: "IX_Respuesta_EncuestaActividadId",
                table: "Respuesta");

            migrationBuilder.DropColumn(
                name: "EncuestaActividadId",
                table: "Respuesta");

            migrationBuilder.AlterColumn<Guid>(
                name: "AlumnoId",
                table: "Respuesta",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ActividadId",
                table: "Respuesta",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AlumnoId1",
                table: "Respuesta",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_AlumnoId1",
                table: "Respuesta",
                column: "AlumnoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_AspNetUsers_AlumnoId1",
                table: "Respuesta",
                column: "AlumnoId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
