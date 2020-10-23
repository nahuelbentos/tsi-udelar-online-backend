using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ActualizacionUsuarioFacultad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserName_udelar",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "EmailPersonal",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FacultadId1",
                table: "AspNetUsers",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Facultad_FacultadId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FacultadId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmailPersonal",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FacultadId1",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName_udelar",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
