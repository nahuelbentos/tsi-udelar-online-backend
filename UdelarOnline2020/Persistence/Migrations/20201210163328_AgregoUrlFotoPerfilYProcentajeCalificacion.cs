using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AgregoUrlFotoPerfilYProcentajeCalificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreguntaRespuesta_Actividad_PruebaOnlineActividadId",
                table: "PreguntaRespuesta");

            migrationBuilder.DropForeignKey(
                name: "FK_RespuestaPrueba_AlumnoPruebaOnline_AlumnoPruebaOnlineAlumnoId_AlumnoPruebaOnlinePruebaOnlineId",
                table: "RespuestaPrueba");

            migrationBuilder.DropForeignKey(
                name: "FK_TemaForo_Foro_ForoId",
                table: "TemaForo");

            migrationBuilder.AlterColumn<Guid>(
                name: "ForoId",
                table: "TemaForo",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AlumnoPruebaOnlinePruebaOnlineId",
                table: "RespuestaPrueba",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AlumnoPruebaOnlineAlumnoId",
                table: "RespuestaPrueba",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlumnoId",
                table: "RespuestaPrueba",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PruebaOnlineActividadId",
                table: "RespuestaPrueba",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PruebaOnlineActividadId",
                table: "PreguntaRespuesta",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlFotoPerfil",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CalificacionPorcentaje",
                table: "AlumnoPruebaOnline",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaPrueba_AlumnoId",
                table: "RespuestaPrueba",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaPrueba_PruebaOnlineActividadId",
                table: "RespuestaPrueba",
                column: "PruebaOnlineActividadId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreguntaRespuesta_Actividad_PruebaOnlineActividadId",
                table: "PreguntaRespuesta",
                column: "PruebaOnlineActividadId",
                principalTable: "Actividad",
                principalColumn: "ActividadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestaPrueba_AspNetUsers_AlumnoId",
                table: "RespuestaPrueba",
                column: "AlumnoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestaPrueba_Actividad_PruebaOnlineActividadId",
                table: "RespuestaPrueba",
                column: "PruebaOnlineActividadId",
                principalTable: "Actividad",
                principalColumn: "ActividadId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestaPrueba_AlumnoPruebaOnline_AlumnoPruebaOnlineAlumnoId_AlumnoPruebaOnlinePruebaOnlineId",
                table: "RespuestaPrueba",
                columns: new[] { "AlumnoPruebaOnlineAlumnoId", "AlumnoPruebaOnlinePruebaOnlineId" },
                principalTable: "AlumnoPruebaOnline",
                principalColumns: new[] { "AlumnoId", "PruebaOnlineId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemaForo_Foro_ForoId",
                table: "TemaForo",
                column: "ForoId",
                principalTable: "Foro",
                principalColumn: "ForoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreguntaRespuesta_Actividad_PruebaOnlineActividadId",
                table: "PreguntaRespuesta");

            migrationBuilder.DropForeignKey(
                name: "FK_RespuestaPrueba_AspNetUsers_AlumnoId",
                table: "RespuestaPrueba");

            migrationBuilder.DropForeignKey(
                name: "FK_RespuestaPrueba_Actividad_PruebaOnlineActividadId",
                table: "RespuestaPrueba");

            migrationBuilder.DropForeignKey(
                name: "FK_RespuestaPrueba_AlumnoPruebaOnline_AlumnoPruebaOnlineAlumnoId_AlumnoPruebaOnlinePruebaOnlineId",
                table: "RespuestaPrueba");

            migrationBuilder.DropForeignKey(
                name: "FK_TemaForo_Foro_ForoId",
                table: "TemaForo");

            migrationBuilder.DropIndex(
                name: "IX_RespuestaPrueba_AlumnoId",
                table: "RespuestaPrueba");

            migrationBuilder.DropIndex(
                name: "IX_RespuestaPrueba_PruebaOnlineActividadId",
                table: "RespuestaPrueba");

            migrationBuilder.DropColumn(
                name: "AlumnoId",
                table: "RespuestaPrueba");

            migrationBuilder.DropColumn(
                name: "PruebaOnlineActividadId",
                table: "RespuestaPrueba");

            migrationBuilder.DropColumn(
                name: "UrlFotoPerfil",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CalificacionPorcentaje",
                table: "AlumnoPruebaOnline");

            migrationBuilder.AlterColumn<Guid>(
                name: "ForoId",
                table: "TemaForo",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "AlumnoPruebaOnlinePruebaOnlineId",
                table: "RespuestaPrueba",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "AlumnoPruebaOnlineAlumnoId",
                table: "RespuestaPrueba",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "PruebaOnlineActividadId",
                table: "PreguntaRespuesta",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_PreguntaRespuesta_Actividad_PruebaOnlineActividadId",
                table: "PreguntaRespuesta",
                column: "PruebaOnlineActividadId",
                principalTable: "Actividad",
                principalColumn: "ActividadId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestaPrueba_AlumnoPruebaOnline_AlumnoPruebaOnlineAlumnoId_AlumnoPruebaOnlinePruebaOnlineId",
                table: "RespuestaPrueba",
                columns: new[] { "AlumnoPruebaOnlineAlumnoId", "AlumnoPruebaOnlinePruebaOnlineId" },
                principalTable: "AlumnoPruebaOnline",
                principalColumns: new[] { "AlumnoId", "PruebaOnlineId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TemaForo_Foro_ForoId",
                table: "TemaForo",
                column: "ForoId",
                principalTable: "Foro",
                principalColumn: "ForoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
