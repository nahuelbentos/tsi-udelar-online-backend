using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AgregoPruebaOnlineYCambiosReferentesABedelias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comunicado_AspNetUsers_UsuarioId",
                table: "Comunicado");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Comunicado",
                newName: "usuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Comunicado_UsuarioId",
                table: "Comunicado",
                newName: "IX_Comunicado_usuarioId");

            migrationBuilder.AddColumn<bool>(
                name: "ActaCerrada",
                table: "Curso",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Calificacion",
                table: "AlumnoCurso",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActaCerrada",
                table: "AlumnoCurso",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "AlumnoCurso",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Inscripto",
                table: "AlumnoCurso",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "FacultadId",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PruebaOnline_Descripcion",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MinutosExpiracion",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PruebaOnline_Nombre",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PruebaOnlineId",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Actividad",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AlumnoPruebaOnline",
                columns: table => new
                {
                    AlumnoId = table.Column<Guid>(nullable: false),
                    PruebaOnlineId = table.Column<Guid>(nullable: false),
                    AlumnoId1 = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    FechaExpiracion = table.Column<DateTime>(nullable: false),
                    Nota = table.Column<int>(nullable: false),
                    Inscripto = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnoPruebaOnline", x => new { x.AlumnoId, x.PruebaOnlineId });
                    table.ForeignKey(
                        name: "FK_AlumnoPruebaOnline_AspNetUsers_AlumnoId1",
                        column: x => x.AlumnoId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlumnoPruebaOnline_Actividad_PruebaOnlineId",
                        column: x => x.PruebaOnlineId,
                        principalTable: "Actividad",
                        principalColumn: "ActividadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreguntaRespuesta",
                columns: table => new
                {
                    PreguntaRespuestaId = table.Column<Guid>(nullable: false),
                    Pregunta = table.Column<string>(nullable: true),
                    RespuestaCorrecta = table.Column<int>(nullable: false),
                    Puntos = table.Column<int>(nullable: false),
                    Resta = table.Column<bool>(nullable: false),
                    PruebaOnlineActividadId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntaRespuesta", x => x.PreguntaRespuestaId);
                    table.ForeignKey(
                        name: "FK_PreguntaRespuesta_Actividad_PruebaOnlineActividadId",
                        column: x => x.PruebaOnlineActividadId,
                        principalTable: "Actividad",
                        principalColumn: "ActividadId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DtPruebaRespuesta",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    respuesta = table.Column<string>(nullable: true),
                    PreguntaRespuestaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DtPruebaRespuesta", x => x.id);
                    table.ForeignKey(
                        name: "FK_DtPruebaRespuesta_PreguntaRespuesta_PreguntaRespuestaId",
                        column: x => x.PreguntaRespuestaId,
                        principalTable: "PreguntaRespuesta",
                        principalColumn: "PreguntaRespuestaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_FacultadId",
                table: "Actividad",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoPruebaOnline_AlumnoId1",
                table: "AlumnoPruebaOnline",
                column: "AlumnoId1");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoPruebaOnline_PruebaOnlineId",
                table: "AlumnoPruebaOnline",
                column: "PruebaOnlineId");

            migrationBuilder.CreateIndex(
                name: "IX_DtPruebaRespuesta_PreguntaRespuestaId",
                table: "DtPruebaRespuesta",
                column: "PreguntaRespuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntaRespuesta_PruebaOnlineActividadId",
                table: "PreguntaRespuesta",
                column: "PruebaOnlineActividadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_Facultad_FacultadId",
                table: "Actividad",
                column: "FacultadId",
                principalTable: "Facultad",
                principalColumn: "FacultadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comunicado_AspNetUsers_usuarioId",
                table: "Comunicado",
                column: "usuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Facultad_FacultadId",
                table: "Actividad");

            migrationBuilder.DropForeignKey(
                name: "FK_Comunicado_AspNetUsers_usuarioId",
                table: "Comunicado");

            migrationBuilder.DropTable(
                name: "AlumnoPruebaOnline");

            migrationBuilder.DropTable(
                name: "DtPruebaRespuesta");

            migrationBuilder.DropTable(
                name: "PreguntaRespuesta");

            migrationBuilder.DropIndex(
                name: "IX_Actividad_FacultadId",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "ActaCerrada",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "Calificacion",
                table: "AlumnoCurso");

            migrationBuilder.DropColumn(
                name: "FechaActaCerrada",
                table: "AlumnoCurso");

            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "AlumnoCurso");

            migrationBuilder.DropColumn(
                name: "Inscripto",
                table: "AlumnoCurso");

            migrationBuilder.DropColumn(
                name: "FacultadId",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "PruebaOnline_Descripcion",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "MinutosExpiracion",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "PruebaOnline_Nombre",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "PruebaOnlineId",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Actividad");

            migrationBuilder.RenameColumn(
                name: "usuarioId",
                table: "Comunicado",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Comunicado_usuarioId",
                table: "Comunicado",
                newName: "IX_Comunicado_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comunicado_AspNetUsers_UsuarioId",
                table: "Comunicado",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
