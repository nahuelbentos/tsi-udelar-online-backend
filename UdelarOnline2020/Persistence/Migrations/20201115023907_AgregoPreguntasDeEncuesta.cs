using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AgregoPreguntasDeEncuesta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PreguntaId",
                table: "Respuesta",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pregunta",
                columns: table => new
                {
                    PreguntaId = table.Column<Guid>(nullable: false),
                    Texto = table.Column<string>(nullable: true),
                    EncuestaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregunta", x => x.PreguntaId);
                    table.ForeignKey(
                        name: "FK_Pregunta_Actividad_EncuestaId",
                        column: x => x.EncuestaId,
                        principalTable: "Actividad",
                        principalColumn: "ActividadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_PreguntaId",
                table: "Respuesta",
                column: "PreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pregunta_EncuestaId",
                table: "Pregunta",
                column: "EncuestaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_Pregunta_PreguntaId",
                table: "Respuesta",
                column: "PreguntaId",
                principalTable: "Pregunta",
                principalColumn: "PreguntaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Pregunta_PreguntaId",
                table: "Respuesta");

            migrationBuilder.DropTable(
                name: "Pregunta");

            migrationBuilder.DropIndex(
                name: "IX_Respuesta_PreguntaId",
                table: "Respuesta");

            migrationBuilder.DropColumn(
                name: "PreguntaId",
                table: "Respuesta");
        }
    }
}
