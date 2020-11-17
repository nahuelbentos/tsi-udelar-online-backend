using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AgregoCamposEnFacultadYCambioArchivoEnTodasLasTablasQueLoTengan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchivoAdjunto",
                table: "TemaForo");

            migrationBuilder.DropColumn(
                name: "Archivo",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "Archivo",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "Trabajo_Archivo",
                table: "Actividad");

            migrationBuilder.AddColumn<byte[]>(
                name: "ArchivoData",
                table: "TemaForo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArchivoExtension",
                table: "TemaForo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArchivoNombre",
                table: "TemaForo",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ArchivoData",
                table: "Material",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArchivoExtension",
                table: "Material",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArchivoNombre",
                table: "Material",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Material",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Material",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorCodigo",
                table: "Facultad",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoData",
                table: "Facultad",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoExtension",
                table: "Facultad",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoNombre",
                table: "Facultad",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ArchivoData",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArchivoExtension",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArchivoNombre",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Encuesta_Descripcion",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Encuesta_Nombre",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Trabajo_ArchivoData",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trabajo_ArchivoExtension",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trabajo_ArchivoNombre",
                table: "Actividad",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchivoData",
                table: "TemaForo");

            migrationBuilder.DropColumn(
                name: "ArchivoExtension",
                table: "TemaForo");

            migrationBuilder.DropColumn(
                name: "ArchivoNombre",
                table: "TemaForo");

            migrationBuilder.DropColumn(
                name: "ArchivoData",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "ArchivoExtension",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "ArchivoNombre",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "ColorCodigo",
                table: "Facultad");

            migrationBuilder.DropColumn(
                name: "LogoData",
                table: "Facultad");

            migrationBuilder.DropColumn(
                name: "LogoExtension",
                table: "Facultad");

            migrationBuilder.DropColumn(
                name: "LogoNombre",
                table: "Facultad");

            migrationBuilder.DropColumn(
                name: "ArchivoData",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "ArchivoExtension",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "ArchivoNombre",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "Encuesta_Descripcion",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "Encuesta_Nombre",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "Trabajo_ArchivoData",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "Trabajo_ArchivoExtension",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "Trabajo_ArchivoNombre",
                table: "Actividad");

            migrationBuilder.AddColumn<byte[]>(
                name: "ArchivoAdjunto",
                table: "TemaForo",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Archivo",
                table: "Material",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Archivo",
                table: "Actividad",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Trabajo_Archivo",
                table: "Actividad",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
