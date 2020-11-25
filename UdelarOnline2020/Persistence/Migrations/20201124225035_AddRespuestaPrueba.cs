using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddRespuestaPrueba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Facultad_FacultadId",
                table: "Actividad");

            migrationBuilder.CreateTable(
                name: "RespuestaPrueba",
                columns: table => new
                {
                    RespuestaPruebaId = table.Column<Guid>(nullable: false),
                    PreguntaId = table.Column<Guid>(nullable: false),
                    PreguntaRespuestaId = table.Column<Guid>(nullable: true),
                    RespuestaId = table.Column<int>(nullable: false),
                    AlumnoPruebaOnlineAlumnoId = table.Column<Guid>(nullable: true),
                    AlumnoPruebaOnlinePruebaOnlineId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestaPrueba", x => x.RespuestaPruebaId);
                    table.ForeignKey(
                        name: "FK_RespuestaPrueba_PreguntaRespuesta_PreguntaRespuestaId",
                        column: x => x.PreguntaRespuestaId,
                        principalTable: "PreguntaRespuesta",
                        principalColumn: "PreguntaRespuestaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespuestaPrueba_AlumnoPruebaOnline_AlumnoPruebaOnlineAlumnoId_AlumnoPruebaOnlinePruebaOnlineId",
                        columns: x => new { x.AlumnoPruebaOnlineAlumnoId, x.AlumnoPruebaOnlinePruebaOnlineId },
                        principalTable: "AlumnoPruebaOnline",
                        principalColumns: new[] { "AlumnoId", "PruebaOnlineId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaPrueba_PreguntaRespuestaId",
                table: "RespuestaPrueba",
                column: "PreguntaRespuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaPrueba_AlumnoPruebaOnlineAlumnoId_AlumnoPruebaOnlinePruebaOnlineId",
                table: "RespuestaPrueba",
                columns: new[] { "AlumnoPruebaOnlineAlumnoId", "AlumnoPruebaOnlinePruebaOnlineId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_Facultad_FacultadId",
                table: "Actividad",
                column: "FacultadId",
                principalTable: "Facultad",
                principalColumn: "FacultadId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Facultad_FacultadId",
                table: "Actividad");

            migrationBuilder.DropTable(
                name: "RespuestaPrueba");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_Facultad_FacultadId",
                table: "Actividad",
                column: "FacultadId",
                principalTable: "Facultad",
                principalColumn: "FacultadId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
