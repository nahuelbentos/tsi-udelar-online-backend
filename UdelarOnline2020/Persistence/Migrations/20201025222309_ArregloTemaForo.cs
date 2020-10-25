using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ArregloTemaForo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemaForo_AspNetUsers_EmisorId1",
                table: "TemaForo");

            migrationBuilder.DropIndex(
                name: "IX_TemaForo_EmisorId1",
                table: "TemaForo");

            migrationBuilder.DropColumn(
                name: "EmisorId1",
                table: "TemaForo");

            migrationBuilder.AlterColumn<string>(
                name: "EmisorId",
                table: "TemaForo",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "ArchivoAdjunto",
                table: "TemaForo",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ActividadId",
                table: "Respuesta",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EncuestaActividadId",
                table: "Respuesta",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemaForo_EmisorId",
                table: "TemaForo",
                column: "EmisorId");

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
                name: "FK_TemaForo_AspNetUsers_EmisorId",
                table: "TemaForo",
                column: "EmisorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Actividad_EncuestaActividadId",
                table: "Respuesta");

            migrationBuilder.DropForeignKey(
                name: "FK_TemaForo_AspNetUsers_EmisorId",
                table: "TemaForo");

            migrationBuilder.DropIndex(
                name: "IX_TemaForo_EmisorId",
                table: "TemaForo");

            migrationBuilder.DropIndex(
                name: "IX_Respuesta_EncuestaActividadId",
                table: "Respuesta");

            migrationBuilder.DropColumn(
                name: "ArchivoAdjunto",
                table: "TemaForo");

            migrationBuilder.DropColumn(
                name: "ActividadId",
                table: "Respuesta");

            migrationBuilder.DropColumn(
                name: "EncuestaActividadId",
                table: "Respuesta");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmisorId",
                table: "TemaForo",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmisorId1",
                table: "TemaForo",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemaForo_EmisorId1",
                table: "TemaForo",
                column: "EmisorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TemaForo_AspNetUsers_EmisorId1",
                table: "TemaForo",
                column: "EmisorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
