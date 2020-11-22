using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class FixUsuarioCurso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioCurso_AspNetUsers_UsuarioId1",
                table: "UsuarioCurso");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioCurso_UsuarioId1",
                table: "UsuarioCurso");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "UsuarioCurso");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "UsuarioCurso",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioCurso_AspNetUsers_UsuarioId",
                table: "UsuarioCurso",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioCurso_AspNetUsers_UsuarioId",
                table: "UsuarioCurso");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "UsuarioCurso",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "UsuarioCurso",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCurso_UsuarioId1",
                table: "UsuarioCurso",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioCurso_AspNetUsers_UsuarioId1",
                table: "UsuarioCurso",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
