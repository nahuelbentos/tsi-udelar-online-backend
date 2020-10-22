using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class EliminoReferenciaAdministradorFacultadFacultadEnContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdministradorFacultadFacultad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdministradorFacultadFacultad",
                columns: table => new
                {
                    AdministradorFacultadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacultadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdministradorFacultadId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministradorFacultadFacultad", x => new { x.AdministradorFacultadId, x.FacultadId });
                    table.ForeignKey(
                        name: "FK_AdministradorFacultadFacultad_AspNetUsers_AdministradorFacultadId1",
                        column: x => x.AdministradorFacultadId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdministradorFacultadFacultad_Facultad_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "Facultad",
                        principalColumn: "FacultadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdministradorFacultadFacultad_AdministradorFacultadId1",
                table: "AdministradorFacultadFacultad",
                column: "AdministradorFacultadId1");

            migrationBuilder.CreateIndex(
                name: "IX_AdministradorFacultadFacultad_FacultadId",
                table: "AdministradorFacultadFacultad",
                column: "FacultadId");
        }
    }
}
