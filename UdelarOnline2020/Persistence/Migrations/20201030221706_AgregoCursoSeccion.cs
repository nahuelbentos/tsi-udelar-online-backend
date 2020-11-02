using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AgregoCursoSeccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Curso_CursoId",
                table: "Actividad");

            migrationBuilder.DropForeignKey(
                name: "FK_Curso_TemplateCurso_TemplateCursoId",
                table: "Curso");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Curso_CursoId",
                table: "Material");

            migrationBuilder.DropTable(
                name: "CursoForo");

            migrationBuilder.DropIndex(
                name: "IX_Material_CursoId",
                table: "Material");

            migrationBuilder.DropIndex(
                name: "IX_Actividad_CursoId",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "ArchivoAdjunto",
                table: "TemaForo");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Actividad");

            migrationBuilder.AddColumn<byte[]>(
                name: "Archivo",
                table: "Material",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CursoSeccionCursoId",
                table: "Material",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CursoSeccionSeccionId",
                table: "Material",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CursoSeccionCursoId",
                table: "Foro",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CursoSeccionSeccionId",
                table: "Foro",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TemplateCursoId",
                table: "Curso",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CursoSeccionCursoId",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CursoSeccionSeccionId",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Archivo",
                table: "Actividad",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Trabajo_Archivo",
                table: "Actividad",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Seccion",
                columns: table => new
                {
                    SeccionId = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsVisible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seccion", x => x.SeccionId);
                });

            migrationBuilder.CreateTable(
                name: "CursoSeccion",
                columns: table => new
                {
                    CursoId = table.Column<Guid>(nullable: false),
                    SeccionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoSeccion", x => new { x.CursoId, x.SeccionId });
                    table.ForeignKey(
                        name: "FK_CursoSeccion_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoSeccion_Seccion_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Seccion",
                        principalColumn: "SeccionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursoSeccionActividad",
                columns: table => new
                {
                    CursoId = table.Column<Guid>(nullable: false),
                    SeccionId = table.Column<Guid>(nullable: false),
                    ActividadId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoSeccionActividad", x => new { x.CursoId, x.SeccionId, x.ActividadId });
                    table.ForeignKey(
                        name: "FK_CursoSeccionActividad_Actividad_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "Actividad",
                        principalColumn: "ActividadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoSeccionActividad_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoSeccionActividad_Seccion_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Seccion",
                        principalColumn: "SeccionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursoSeccionForo",
                columns: table => new
                {
                    CursoId = table.Column<Guid>(nullable: false),
                    SeccionId = table.Column<Guid>(nullable: false),
                    ForoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoSeccionForo", x => new { x.CursoId, x.SeccionId, x.ForoId });
                    table.ForeignKey(
                        name: "FK_CursoSeccionForo_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoSeccionForo_Foro_ForoId",
                        column: x => x.ForoId,
                        principalTable: "Foro",
                        principalColumn: "ForoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoSeccionForo_Seccion_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Seccion",
                        principalColumn: "SeccionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursoSeccionMaterial",
                columns: table => new
                {
                    CursoId = table.Column<Guid>(nullable: false),
                    SeccionId = table.Column<Guid>(nullable: false),
                    MaterialId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoSeccionMaterial", x => new { x.CursoId, x.SeccionId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_CursoSeccionMaterial_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoSeccionMaterial_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoSeccionMaterial_Seccion_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Seccion",
                        principalColumn: "SeccionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateCursoSeccion",
                columns: table => new
                {
                    TemplateCursoId = table.Column<Guid>(nullable: false),
                    SeccionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateCursoSeccion", x => new { x.TemplateCursoId, x.SeccionId });
                    table.ForeignKey(
                        name: "FK_TemplateCursoSeccion_Seccion_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Seccion",
                        principalColumn: "SeccionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateCursoSeccion_TemplateCurso_TemplateCursoId",
                        column: x => x.TemplateCursoId,
                        principalTable: "TemplateCurso",
                        principalColumn: "TemplateCursoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Material_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Material",
                columns: new[] { "CursoSeccionCursoId", "CursoSeccionSeccionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Foro_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Foro",
                columns: new[] { "CursoSeccionCursoId", "CursoSeccionSeccionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Actividad",
                columns: new[] { "CursoSeccionCursoId", "CursoSeccionSeccionId" });

            migrationBuilder.CreateIndex(
                name: "IX_CursoSeccion_SeccionId",
                table: "CursoSeccion",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoSeccionActividad_ActividadId",
                table: "CursoSeccionActividad",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoSeccionActividad_SeccionId",
                table: "CursoSeccionActividad",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoSeccionForo_ForoId",
                table: "CursoSeccionForo",
                column: "ForoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoSeccionForo_SeccionId",
                table: "CursoSeccionForo",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoSeccionMaterial_MaterialId",
                table: "CursoSeccionMaterial",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoSeccionMaterial_SeccionId",
                table: "CursoSeccionMaterial",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateCursoSeccion_SeccionId",
                table: "TemplateCursoSeccion",
                column: "SeccionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_CursoSeccion_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Actividad",
                columns: new[] { "CursoSeccionCursoId", "CursoSeccionSeccionId" },
                principalTable: "CursoSeccion",
                principalColumns: new[] { "CursoId", "SeccionId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_TemplateCurso_TemplateCursoId",
                table: "Curso",
                column: "TemplateCursoId",
                principalTable: "TemplateCurso",
                principalColumn: "TemplateCursoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foro_CursoSeccion_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Foro",
                columns: new[] { "CursoSeccionCursoId", "CursoSeccionSeccionId" },
                principalTable: "CursoSeccion",
                principalColumns: new[] { "CursoId", "SeccionId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_CursoSeccion_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Material",
                columns: new[] { "CursoSeccionCursoId", "CursoSeccionSeccionId" },
                principalTable: "CursoSeccion",
                principalColumns: new[] { "CursoId", "SeccionId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_CursoSeccion_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Actividad");

            migrationBuilder.DropForeignKey(
                name: "FK_Curso_TemplateCurso_TemplateCursoId",
                table: "Curso");

            migrationBuilder.DropForeignKey(
                name: "FK_Foro_CursoSeccion_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Foro");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_CursoSeccion_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Material");

            migrationBuilder.DropTable(
                name: "CursoSeccion");

            migrationBuilder.DropTable(
                name: "CursoSeccionActividad");

            migrationBuilder.DropTable(
                name: "CursoSeccionForo");

            migrationBuilder.DropTable(
                name: "CursoSeccionMaterial");

            migrationBuilder.DropTable(
                name: "TemplateCursoSeccion");

            migrationBuilder.DropTable(
                name: "Seccion");

            migrationBuilder.DropIndex(
                name: "IX_Material_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Material");

            migrationBuilder.DropIndex(
                name: "IX_Foro_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Foro");

            migrationBuilder.DropIndex(
                name: "IX_Actividad_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "Archivo",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "CursoSeccionCursoId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "CursoSeccionSeccionId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "CursoSeccionCursoId",
                table: "Foro");

            migrationBuilder.DropColumn(
                name: "CursoSeccionSeccionId",
                table: "Foro");

            migrationBuilder.DropColumn(
                name: "CursoSeccionCursoId",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "CursoSeccionSeccionId",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "Archivo",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "Trabajo_Archivo",
                table: "Actividad");

            migrationBuilder.AddColumn<string>(
                name: "ArchivoAdjunto",
                table: "TemaForo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CursoId",
                table: "Material",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TemplateCursoId",
                table: "Curso",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CursoId",
                table: "Actividad",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CursoForo",
                columns: table => new
                {
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoForo", x => new { x.CursoId, x.ForoId });
                    table.ForeignKey(
                        name: "FK_CursoForo_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoForo_Foro_ForoId",
                        column: x => x.ForoId,
                        principalTable: "Foro",
                        principalColumn: "ForoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Material_CursoId",
                table: "Material",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_CursoId",
                table: "Actividad",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoForo_ForoId",
                table: "CursoForo",
                column: "ForoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_Curso_CursoId",
                table: "Actividad",
                column: "CursoId",
                principalTable: "Curso",
                principalColumn: "CursoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_TemplateCurso_TemplateCursoId",
                table: "Curso",
                column: "TemplateCursoId",
                principalTable: "TemplateCurso",
                principalColumn: "TemplateCursoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Curso_CursoId",
                table: "Material",
                column: "CursoId",
                principalTable: "Curso",
                principalColumn: "CursoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
