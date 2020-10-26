using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class QuitoFacultadIdDeUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Facultad_FacultadId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FacultadId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FacultadId1",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "FacultadId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FacultadId",
                table: "AspNetUsers",
                column: "FacultadId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Facultad_FacultadId",
                table: "AspNetUsers",
                column: "FacultadId",
                principalTable: "Facultad",
                principalColumn: "FacultadId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Facultad_FacultadId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FacultadId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FacultadId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "FacultadId1",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FacultadId1",
                table: "AspNetUsers",
                column: "FacultadId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Facultad_FacultadId1",
                table: "AspNetUsers",
                column: "FacultadId1",
                principalTable: "Facultad",
                principalColumn: "FacultadId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
