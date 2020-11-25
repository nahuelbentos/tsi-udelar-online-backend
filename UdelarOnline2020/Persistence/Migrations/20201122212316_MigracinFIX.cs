using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class MigracinFIX : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facultad",
                columns: table => new
                {
                    FacultadId = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    UrlAcceso = table.Column<string>(nullable: true),
                    DominioMail = table.Column<string>(nullable: true),
                    LogoNombre = table.Column<string>(nullable: true),
                    LogoExtension = table.Column<string>(nullable: true),
                    LogoData = table.Column<string>(nullable: true),
                    ColorCodigo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultad", x => x.FacultadId);
                });

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
                name: "TemplateCurso",
                columns: table => new
                {
                    TemplateCursoId = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateCurso", x => x.TemplateCursoId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Nombres = table.Column<string>(nullable: true),
                    Apellidos = table.Column<string>(nullable: true),
                    CI = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    Direccion = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    EmailPersonal = table.Column<string>(nullable: true),
                    FacultadId = table.Column<Guid>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Facultad_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "Facultad",
                        principalColumn: "FacultadId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carrera",
                columns: table => new
                {
                    CarreraId = table.Column<Guid>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    FacultadId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrera", x => x.CarreraId);
                    table.ForeignKey(
                        name: "FK_Carrera_Facultad_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "Facultad",
                        principalColumn: "FacultadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    CursoId = table.Column<Guid>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Modalidad = table.Column<int>(nullable: false),
                    RequiereMatriculacion = table.Column<bool>(nullable: false),
                    SalaVirtual = table.Column<string>(nullable: true),
                    TemplateCursoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.CursoId);
                    table.ForeignKey(
                        name: "FK_Curso_TemplateCurso_TemplateCursoId",
                        column: x => x.TemplateCursoId,
                        principalTable: "TemplateCurso",
                        principalColumn: "TemplateCursoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemplateCursoSeccion",
                columns: table => new
                {
                    TemplateCursoId = table.Column<Guid>(nullable: false),
                    SeccionId = table.Column<Guid>(nullable: false),
                    TemplateCursoSeccionId = table.Column<Guid>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comunicado",
                columns: table => new
                {
                    ComunicadoId = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    usuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunicado", x => x.ComunicadoId);
                    table.ForeignKey(
                        name: "FK_Comunicado_AspNetUsers_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlumnoCurso",
                columns: table => new
                {
                    AlumnoId = table.Column<Guid>(nullable: false),
                    CursoId = table.Column<Guid>(nullable: false),
                    AlumnoId1 = table.Column<string>(nullable: true),
                    Inscripto = table.Column<bool>(nullable: false),
                    Calificacion = table.Column<int>(nullable: false),
                    Feedback = table.Column<string>(nullable: true),
                    FechaActaCerrada = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnoCurso", x => new { x.AlumnoId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_AlumnoCurso_AspNetUsers_AlumnoId1",
                        column: x => x.AlumnoId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlumnoCurso_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarreraCurso",
                columns: table => new
                {
                    CarreraId = table.Column<Guid>(nullable: false),
                    CursoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarreraCurso", x => new { x.CarreraId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_CarreraCurso_Carrera_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carrera",
                        principalColumn: "CarreraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarreraCurso_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursoSeccion",
                columns: table => new
                {
                    CursoId = table.Column<Guid>(nullable: false),
                    SeccionId = table.Column<Guid>(nullable: false),
                    CursoSeccionId = table.Column<Guid>(nullable: false)
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
                name: "UsuarioCurso",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(nullable: false),
                    CursoId = table.Column<Guid>(nullable: false),
                    UsuarioId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCurso", x => new { x.UsuarioId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_UsuarioCurso_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioCurso_AspNetUsers_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComunicadoCurso",
                columns: table => new
                {
                    ComunicadoId = table.Column<Guid>(nullable: false),
                    CursoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComunicadoCurso", x => new { x.ComunicadoId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_ComunicadoCurso_Comunicado_ComunicadoId",
                        column: x => x.ComunicadoId,
                        principalTable: "Comunicado",
                        principalColumn: "ComunicadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComunicadoCurso_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComunicadoFacultad",
                columns: table => new
                {
                    ComunicadoId = table.Column<Guid>(nullable: false),
                    FacultadId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComunicadoFacultad", x => new { x.ComunicadoId, x.FacultadId });
                    table.ForeignKey(
                        name: "FK_ComunicadoFacultad_Comunicado_ComunicadoId",
                        column: x => x.ComunicadoId,
                        principalTable: "Comunicado",
                        principalColumn: "ComunicadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComunicadoFacultad_Facultad_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "Facultad",
                        principalColumn: "FacultadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actividad",
                columns: table => new
                {
                    ActividadId = table.Column<Guid>(nullable: false),
                    FechaRealizada = table.Column<DateTime>(nullable: false),
                    FechaFinalizada = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    CursoSeccionCursoId = table.Column<Guid>(nullable: true),
                    CursoSeccionSeccionId = table.Column<Guid>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ArchivoData = table.Column<byte[]>(nullable: true),
                    ArchivoNombre = table.Column<string>(nullable: true),
                    ArchivoExtension = table.Column<string>(nullable: true),
                    EsAdministrador = table.Column<bool>(nullable: true),
                    FacultadId = table.Column<Guid>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    MinutosExpiracion = table.Column<int>(nullable: true),
                    Activa = table.Column<bool>(nullable: true),
                    Trabajo_ArchivoData = table.Column<byte[]>(nullable: true),
                    Trabajo_ArchivoNombre = table.Column<string>(nullable: true),
                    Trabajo_ArchivoExtension = table.Column<string>(nullable: true),
                    EsIndividual = table.Column<bool>(nullable: true),
                    Calificacion = table.Column<int>(nullable: true),
                    Nota = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividad", x => x.ActividadId);
                    table.ForeignKey(
                        name: "FK_Actividad_CursoSeccion_CursoSeccionCursoId_CursoSeccionSeccionId",
                        columns: x => new { x.CursoSeccionCursoId, x.CursoSeccionSeccionId },
                        principalTable: "CursoSeccion",
                        principalColumns: new[] { "CursoId", "SeccionId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actividad_Facultad_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "Facultad",
                        principalColumn: "FacultadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foro",
                columns: table => new
                {
                    ForoId = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    CursoSeccionCursoId = table.Column<Guid>(nullable: true),
                    CursoSeccionSeccionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foro", x => x.ForoId);
                    table.ForeignKey(
                        name: "FK_Foro_CursoSeccion_CursoSeccionCursoId_CursoSeccionSeccionId",
                        columns: x => new { x.CursoSeccionCursoId, x.CursoSeccionSeccionId },
                        principalTable: "CursoSeccion",
                        principalColumns: new[] { "CursoId", "SeccionId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialId = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    ArchivoData = table.Column<byte[]>(nullable: true),
                    ArchivoNombre = table.Column<string>(nullable: true),
                    ArchivoExtension = table.Column<string>(nullable: true),
                    CursoSeccionCursoId = table.Column<Guid>(nullable: true),
                    CursoSeccionSeccionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialId);
                    table.ForeignKey(
                        name: "FK_Material_CursoSeccion_CursoSeccionCursoId_CursoSeccionSeccionId",
                        columns: x => new { x.CursoSeccionCursoId, x.CursoSeccionSeccionId },
                        principalTable: "CursoSeccion",
                        principalColumns: new[] { "CursoId", "SeccionId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlumnoClaseDictada",
                columns: table => new
                {
                    AlumnoId = table.Column<Guid>(nullable: false),
                    ClaseDictadaId = table.Column<Guid>(nullable: false),
                    AlumnoId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnoClaseDictada", x => new { x.AlumnoId, x.ClaseDictadaId });
                    table.ForeignKey(
                        name: "FK_AlumnoClaseDictada_AspNetUsers_AlumnoId1",
                        column: x => x.AlumnoId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlumnoClaseDictada_Actividad_ClaseDictadaId",
                        column: x => x.ClaseDictadaId,
                        principalTable: "Actividad",
                        principalColumn: "ActividadId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "AlumnoTrabajo",
                columns: table => new
                {
                    AlumnoId = table.Column<Guid>(nullable: false),
                    TrabajoId = table.Column<Guid>(nullable: false),
                    AlumnoId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnoTrabajo", x => new { x.AlumnoId, x.TrabajoId });
                    table.ForeignKey(
                        name: "FK_AlumnoTrabajo_AspNetUsers_AlumnoId1",
                        column: x => x.AlumnoId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlumnoTrabajo_Actividad_TrabajoId",
                        column: x => x.TrabajoId,
                        principalTable: "Actividad",
                        principalColumn: "ActividadId",
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
                name: "DocenteTrabajo",
                columns: table => new
                {
                    DocenteId = table.Column<Guid>(nullable: false),
                    TrabajoId = table.Column<Guid>(nullable: false),
                    DocenteId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocenteTrabajo", x => new { x.DocenteId, x.TrabajoId });
                    table.ForeignKey(
                        name: "FK_DocenteTrabajo_AspNetUsers_DocenteId1",
                        column: x => x.DocenteId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocenteTrabajo_Actividad_TrabajoId",
                        column: x => x.TrabajoId,
                        principalTable: "Actividad",
                        principalColumn: "ActividadId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "TemaForo",
                columns: table => new
                {
                    TemaForoId = table.Column<Guid>(nullable: false),
                    Asunto = table.Column<string>(nullable: true),
                    Mensaje = table.Column<string>(nullable: true),
                    EmisorId = table.Column<string>(nullable: true),
                    ArchivoData = table.Column<byte[]>(nullable: true),
                    ArchivoNombre = table.Column<string>(nullable: true),
                    ArchivoExtension = table.Column<string>(nullable: true),
                    SubscripcionADiscusion = table.Column<bool>(nullable: false),
                    ForoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemaForo", x => x.TemaForoId);
                    table.ForeignKey(
                        name: "FK_TemaForo_AspNetUsers_EmisorId",
                        column: x => x.EmisorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemaForo_Foro_ForoId",
                        column: x => x.ForoId,
                        principalTable: "Foro",
                        principalColumn: "ForoId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Respuesta",
                columns: table => new
                {
                    RespuestaId = table.Column<Guid>(nullable: false),
                    Mensaje = table.Column<string>(nullable: true),
                    AlumnoId = table.Column<string>(nullable: true),
                    PreguntaId = table.Column<Guid>(nullable: false),
                    FechaRealizada = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuesta", x => x.RespuestaId);
                    table.ForeignKey(
                        name: "FK_Respuesta_AspNetUsers_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Respuesta_Pregunta_PreguntaId",
                        column: x => x.PreguntaId,
                        principalTable: "Pregunta",
                        principalColumn: "PreguntaId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Mensaje",
                columns: table => new
                {
                    MensajeId = table.Column<Guid>(nullable: false),
                    Contenido = table.Column<string>(nullable: true),
                    FechaDeEnviado = table.Column<DateTime>(nullable: false),
                    EmisorId1 = table.Column<string>(nullable: true),
                    EmisorId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ReceptorId = table.Column<Guid>(nullable: true),
                    ReceptorId1 = table.Column<string>(nullable: true),
                    MensajeBloqueado = table.Column<bool>(nullable: true),
                    TemaForoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensaje", x => x.MensajeId);
                    table.ForeignKey(
                        name: "FK_Mensaje_AspNetUsers_EmisorId1",
                        column: x => x.EmisorId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mensaje_AspNetUsers_ReceptorId1",
                        column: x => x.ReceptorId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mensaje_TemaForo_TemaForoId",
                        column: x => x.TemaForoId,
                        principalTable: "TemaForo",
                        principalColumn: "TemaForoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioEnviaMensaje",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(nullable: false),
                    MensajeId = table.Column<Guid>(nullable: false),
                    UsuarioEnviaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioEnviaMensaje", x => new { x.UsuarioId, x.MensajeId });
                    table.ForeignKey(
                        name: "FK_UsuarioEnviaMensaje_Mensaje_MensajeId",
                        column: x => x.MensajeId,
                        principalTable: "Mensaje",
                        principalColumn: "MensajeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioEnviaMensaje_AspNetUsers_UsuarioEnviaId",
                        column: x => x.UsuarioEnviaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRecibeMensaje",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(nullable: false),
                    MensajeId = table.Column<Guid>(nullable: false),
                    UsuarioRecibeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRecibeMensaje", x => new { x.UsuarioId, x.MensajeId });
                    table.ForeignKey(
                        name: "FK_UsuarioRecibeMensaje_Mensaje_MensajeId",
                        column: x => x.MensajeId,
                        principalTable: "Mensaje",
                        principalColumn: "MensajeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRecibeMensaje_AspNetUsers_UsuarioRecibeId",
                        column: x => x.UsuarioRecibeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Actividad",
                columns: new[] { "CursoSeccionCursoId", "CursoSeccionSeccionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_FacultadId",
                table: "Actividad",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoClaseDictada_AlumnoId1",
                table: "AlumnoClaseDictada",
                column: "AlumnoId1");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoClaseDictada_ClaseDictadaId",
                table: "AlumnoClaseDictada",
                column: "ClaseDictadaId");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoCurso_AlumnoId1",
                table: "AlumnoCurso",
                column: "AlumnoId1");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoCurso_CursoId",
                table: "AlumnoCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoPruebaOnline_AlumnoId1",
                table: "AlumnoPruebaOnline",
                column: "AlumnoId1");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoPruebaOnline_PruebaOnlineId",
                table: "AlumnoPruebaOnline",
                column: "PruebaOnlineId");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoTrabajo_AlumnoId1",
                table: "AlumnoTrabajo",
                column: "AlumnoId1");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoTrabajo_TrabajoId",
                table: "AlumnoTrabajo",
                column: "TrabajoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FacultadId",
                table: "AspNetUsers",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Carrera_FacultadId",
                table: "Carrera",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_CarreraCurso_CursoId",
                table: "CarreraCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comunicado_usuarioId",
                table: "Comunicado",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ComunicadoCurso_CursoId",
                table: "ComunicadoCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComunicadoFacultad_FacultadId",
                table: "ComunicadoFacultad",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_TemplateCursoId",
                table: "Curso",
                column: "TemplateCursoId");

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
                name: "IX_DocenteTrabajo_DocenteId1",
                table: "DocenteTrabajo",
                column: "DocenteId1");

            migrationBuilder.CreateIndex(
                name: "IX_DocenteTrabajo_TrabajoId",
                table: "DocenteTrabajo",
                column: "TrabajoId");

            migrationBuilder.CreateIndex(
                name: "IX_DtPruebaRespuesta_PreguntaRespuestaId",
                table: "DtPruebaRespuesta",
                column: "PreguntaRespuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_Foro_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Foro",
                columns: new[] { "CursoSeccionCursoId", "CursoSeccionSeccionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Material_CursoSeccionCursoId_CursoSeccionSeccionId",
                table: "Material",
                columns: new[] { "CursoSeccionCursoId", "CursoSeccionSeccionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Mensaje_EmisorId1",
                table: "Mensaje",
                column: "EmisorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Mensaje_ReceptorId1",
                table: "Mensaje",
                column: "ReceptorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Mensaje_TemaForoId",
                table: "Mensaje",
                column: "TemaForoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pregunta_EncuestaId",
                table: "Pregunta",
                column: "EncuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntaRespuesta_PruebaOnlineActividadId",
                table: "PreguntaRespuesta",
                column: "PruebaOnlineActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_AlumnoId",
                table: "Respuesta",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_PreguntaId",
                table: "Respuesta",
                column: "PreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_TemaForo_EmisorId",
                table: "TemaForo",
                column: "EmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_TemaForo_ForoId",
                table: "TemaForo",
                column: "ForoId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateCursoSeccion_SeccionId",
                table: "TemplateCursoSeccion",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCurso_CursoId",
                table: "UsuarioCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCurso_UsuarioId1",
                table: "UsuarioCurso",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEnviaMensaje_MensajeId",
                table: "UsuarioEnviaMensaje",
                column: "MensajeId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEnviaMensaje_UsuarioEnviaId",
                table: "UsuarioEnviaMensaje",
                column: "UsuarioEnviaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRecibeMensaje_MensajeId",
                table: "UsuarioRecibeMensaje",
                column: "MensajeId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRecibeMensaje_UsuarioRecibeId",
                table: "UsuarioRecibeMensaje",
                column: "UsuarioRecibeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlumnoClaseDictada");

            migrationBuilder.DropTable(
                name: "AlumnoCurso");

            migrationBuilder.DropTable(
                name: "AlumnoPruebaOnline");

            migrationBuilder.DropTable(
                name: "AlumnoTrabajo");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CarreraCurso");

            migrationBuilder.DropTable(
                name: "ComunicadoCurso");

            migrationBuilder.DropTable(
                name: "ComunicadoFacultad");

            migrationBuilder.DropTable(
                name: "CursoSeccionActividad");

            migrationBuilder.DropTable(
                name: "CursoSeccionForo");

            migrationBuilder.DropTable(
                name: "CursoSeccionMaterial");

            migrationBuilder.DropTable(
                name: "DocenteTrabajo");

            migrationBuilder.DropTable(
                name: "DtPruebaRespuesta");

            migrationBuilder.DropTable(
                name: "Respuesta");

            migrationBuilder.DropTable(
                name: "TemplateCursoSeccion");

            migrationBuilder.DropTable(
                name: "UsuarioCurso");

            migrationBuilder.DropTable(
                name: "UsuarioEnviaMensaje");

            migrationBuilder.DropTable(
                name: "UsuarioRecibeMensaje");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Carrera");

            migrationBuilder.DropTable(
                name: "Comunicado");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "PreguntaRespuesta");

            migrationBuilder.DropTable(
                name: "Pregunta");

            migrationBuilder.DropTable(
                name: "Mensaje");

            migrationBuilder.DropTable(
                name: "Actividad");

            migrationBuilder.DropTable(
                name: "TemaForo");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Foro");

            migrationBuilder.DropTable(
                name: "Facultad");

            migrationBuilder.DropTable(
                name: "CursoSeccion");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Seccion");

            migrationBuilder.DropTable(
                name: "TemplateCurso");
        }
    }
}
