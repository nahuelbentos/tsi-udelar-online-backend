using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddUsuarioActividad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Actividad",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_UsuarioId",
                table: "Actividad",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_AspNetUsers_UsuarioId",
                table: "Actividad",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_AspNetUsers_UsuarioId",
                table: "Actividad");

            migrationBuilder.DropIndex(
                name: "IX_Actividad_UsuarioId",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Actividad");
        }
    }
}
