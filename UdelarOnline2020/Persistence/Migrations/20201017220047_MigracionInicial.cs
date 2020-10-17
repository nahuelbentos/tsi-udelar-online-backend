using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class MigracionInicial : Migration
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
                    UserName_udelar = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facultad",
                columns: table => new
                {
                    FacultadId = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    UrlAcceso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultad", x => x.FacultadId);
                });

            migrationBuilder.CreateTable(
                name: "Foro",
                columns: table => new
                {
                    ForoId = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foro", x => x.ForoId);
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
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunicado", x => x.ComunicadoId);
                    table.ForeignKey(
                        name: "FK_Comunicado_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdministradorFacultadFacultad",
                columns: table => new
                {
                    AdministradorFacultadId = table.Column<Guid>(nullable: false),
                    FacultadId = table.Column<Guid>(nullable: false),
                    AdministradorFacultadId1 = table.Column<string>(nullable: true)
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
                name: "TemaForo",
                columns: table => new
                {
                    TemaForoId = table.Column<Guid>(nullable: false),
                    Asunto = table.Column<string>(nullable: true),
                    Mensaje = table.Column<string>(nullable: true),
                    EmisorId = table.Column<Guid>(nullable: false),
                    EmisorId1 = table.Column<string>(nullable: true),
                    SubscripcionADiscusion = table.Column<bool>(nullable: false),
                    ForoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemaForo", x => x.TemaForoId);
                    table.ForeignKey(
                        name: "FK_TemaForo_AspNetUsers_EmisorId1",
                        column: x => x.EmisorId1,
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
                name: "Curso",
                columns: table => new
                {
                    CursoId = table.Column<Guid>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Modalidad = table.Column<int>(nullable: false),
                    RequiereMatriculacion = table.Column<bool>(nullable: false),
                    SalaVirtual = table.Column<string>(nullable: true),
                    TemplateCursoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.CursoId);
                    table.ForeignKey(
                        name: "FK_Curso_TemplateCurso_TemplateCursoId",
                        column: x => x.TemplateCursoId,
                        principalTable: "TemplateCurso",
                        principalColumn: "TemplateCursoId",
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
                name: "Actividad",
                columns: table => new
                {
                    ActividadId = table.Column<Guid>(nullable: false),
                    FechaRealizada = table.Column<DateTime>(nullable: false),
                    FechaFinalizada = table.Column<DateTime>(nullable: false),
                    CursoId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    EsAdministrador = table.Column<bool>(nullable: true),
                    EsIndividual = table.Column<bool>(nullable: true),
                    Calificacion = table.Column<int>(nullable: true),
                    Nota = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividad", x => x.ActividadId);
                    table.ForeignKey(
                        name: "FK_Actividad_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlumnoCurso",
                columns: table => new
                {
                    AlumnoId = table.Column<Guid>(nullable: false),
                    CursoId = table.Column<Guid>(nullable: false),
                    AlumnoId1 = table.Column<string>(nullable: true)
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
                name: "CursoForo",
                columns: table => new
                {
                    CursoId = table.Column<Guid>(nullable: false),
                    ForoId = table.Column<Guid>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialId = table.Column<Guid>(nullable: false),
                    CursoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialId);
                    table.ForeignKey(
                        name: "FK_Material_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Respuesta",
                columns: table => new
                {
                    RespuestaId = table.Column<Guid>(nullable: false),
                    Mensaje = table.Column<string>(nullable: true),
                    AlumnoId1 = table.Column<string>(nullable: true),
                    AlumnoId = table.Column<Guid>(nullable: false),
                    EncuestaActividadId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuesta", x => x.RespuestaId);
                    table.ForeignKey(
                        name: "FK_Respuesta_AspNetUsers_AlumnoId1",
                        column: x => x.AlumnoId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Respuesta_Actividad_EncuestaActividadId",
                        column: x => x.EncuestaActividadId,
                        principalTable: "Actividad",
                        principalColumn: "ActividadId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_CursoId",
                table: "Actividad",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministradorFacultadFacultad_AdministradorFacultadId1",
                table: "AdministradorFacultadFacultad",
                column: "AdministradorFacultadId1");

            migrationBuilder.CreateIndex(
                name: "IX_AdministradorFacultadFacultad_FacultadId",
                table: "AdministradorFacultadFacultad",
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
                name: "IX_Comunicado_UsuarioId",
                table: "Comunicado",
                column: "UsuarioId");

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
                name: "IX_CursoForo_ForoId",
                table: "CursoForo",
                column: "ForoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocenteTrabajo_DocenteId1",
                table: "DocenteTrabajo",
                column: "DocenteId1");

            migrationBuilder.CreateIndex(
                name: "IX_DocenteTrabajo_TrabajoId",
                table: "DocenteTrabajo",
                column: "TrabajoId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_CursoId",
                table: "Material",
                column: "CursoId");

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
                name: "IX_Respuesta_AlumnoId1",
                table: "Respuesta",
                column: "AlumnoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_EncuestaActividadId",
                table: "Respuesta",
                column: "EncuestaActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_TemaForo_EmisorId1",
                table: "TemaForo",
                column: "EmisorId1");

            migrationBuilder.CreateIndex(
                name: "IX_TemaForo_ForoId",
                table: "TemaForo",
                column: "ForoId");

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
                name: "AdministradorFacultadFacultad");

            migrationBuilder.DropTable(
                name: "AlumnoClaseDictada");

            migrationBuilder.DropTable(
                name: "AlumnoCurso");

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
                name: "CursoForo");

            migrationBuilder.DropTable(
                name: "DocenteTrabajo");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Respuesta");

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
                name: "Actividad");

            migrationBuilder.DropTable(
                name: "Mensaje");

            migrationBuilder.DropTable(
                name: "Facultad");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "TemaForo");

            migrationBuilder.DropTable(
                name: "TemplateCurso");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Foro");
        }
    }
}
