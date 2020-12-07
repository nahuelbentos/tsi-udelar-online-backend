using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class QuitoDtPruebaRespuestaDePreguntaRespuesta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DtPruebaRespuesta");

            migrationBuilder.AddColumn<string>(
                name: "Respuesta1",
                table: "PreguntaRespuesta",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Respuesta2",
                table: "PreguntaRespuesta",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Respuesta3",
                table: "PreguntaRespuesta",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Respuesta4",
                table: "PreguntaRespuesta",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Respuesta1",
                table: "PreguntaRespuesta");

            migrationBuilder.DropColumn(
                name: "Respuesta2",
                table: "PreguntaRespuesta");

            migrationBuilder.DropColumn(
                name: "Respuesta3",
                table: "PreguntaRespuesta");

            migrationBuilder.DropColumn(
                name: "Respuesta4",
                table: "PreguntaRespuesta");

            migrationBuilder.CreateTable(
                name: "DtPruebaRespuesta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreguntaRespuestaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    respuesta = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_DtPruebaRespuesta_PreguntaRespuestaId",
                table: "DtPruebaRespuesta",
                column: "PreguntaRespuestaId");
        }
    }
}
