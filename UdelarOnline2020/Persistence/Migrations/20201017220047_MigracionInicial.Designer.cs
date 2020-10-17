﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(UdelarOnlineContext))]
    [Migration("20201017220047_MigracionInicial")]
    partial class MigracionInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Models.Actividad", b =>
                {
                    b.Property<Guid>("ActividadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CursoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaFinalizada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRealizada")
                        .HasColumnType("datetime2");

                    b.HasKey("ActividadId");

                    b.HasIndex("CursoId");

                    b.ToTable("Actividad");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Actividad");
                });

            modelBuilder.Entity("Models.AdministradorFacultadFacultad", b =>
                {
                    b.Property<Guid>("AdministradorFacultadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FacultadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdministradorFacultadId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AdministradorFacultadId", "FacultadId");

                    b.HasIndex("AdministradorFacultadId1");

                    b.HasIndex("FacultadId");

                    b.ToTable("AdministradorFacultadFacultad");
                });

            modelBuilder.Entity("Models.AlumnoClaseDictada", b =>
                {
                    b.Property<Guid>("AlumnoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClaseDictadaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AlumnoId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AlumnoId", "ClaseDictadaId");

                    b.HasIndex("AlumnoId1");

                    b.HasIndex("ClaseDictadaId");

                    b.ToTable("AlumnoClaseDictada");
                });

            modelBuilder.Entity("Models.AlumnoCurso", b =>
                {
                    b.Property<Guid>("AlumnoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CursoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AlumnoId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AlumnoId", "CursoId");

                    b.HasIndex("AlumnoId1");

                    b.HasIndex("CursoId");

                    b.ToTable("AlumnoCurso");
                });

            modelBuilder.Entity("Models.AlumnoTrabajo", b =>
                {
                    b.Property<Guid>("AlumnoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TrabajoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AlumnoId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AlumnoId", "TrabajoId");

                    b.HasIndex("AlumnoId1");

                    b.HasIndex("TrabajoId");

                    b.ToTable("AlumnoTrabajo");
                });

            modelBuilder.Entity("Models.Carrera", b =>
                {
                    b.Property<Guid>("CarreraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FacultadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarreraId");

                    b.HasIndex("FacultadId");

                    b.ToTable("Carrera");
                });

            modelBuilder.Entity("Models.CarreraCurso", b =>
                {
                    b.Property<Guid>("CarreraId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CursoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CarreraId", "CursoId");

                    b.HasIndex("CursoId");

                    b.ToTable("CarreraCurso");
                });

            modelBuilder.Entity("Models.Comunicado", b =>
                {
                    b.Property<Guid>("ComunicadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ComunicadoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Comunicado");
                });

            modelBuilder.Entity("Models.ComunicadoCurso", b =>
                {
                    b.Property<Guid>("ComunicadoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CursoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ComunicadoId", "CursoId");

                    b.HasIndex("CursoId");

                    b.ToTable("ComunicadoCurso");
                });

            modelBuilder.Entity("Models.ComunicadoFacultad", b =>
                {
                    b.Property<Guid>("ComunicadoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FacultadId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ComunicadoId", "FacultadId");

                    b.HasIndex("FacultadId");

                    b.ToTable("ComunicadoFacultad");
                });

            modelBuilder.Entity("Models.Curso", b =>
                {
                    b.Property<Guid>("CursoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Modalidad")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RequiereMatriculacion")
                        .HasColumnType("bit");

                    b.Property<string>("SalaVirtual")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TemplateCursoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CursoId");

                    b.HasIndex("TemplateCursoId");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("Models.CursoForo", b =>
                {
                    b.Property<Guid>("CursoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ForoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CursoId", "ForoId");

                    b.HasIndex("ForoId");

                    b.ToTable("CursoForo");
                });

            modelBuilder.Entity("Models.DocenteTrabajo", b =>
                {
                    b.Property<Guid>("DocenteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TrabajoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DocenteId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DocenteId", "TrabajoId");

                    b.HasIndex("DocenteId1");

                    b.HasIndex("TrabajoId");

                    b.ToTable("DocenteTrabajo");
                });

            modelBuilder.Entity("Models.Facultad", b =>
                {
                    b.Property<Guid>("FacultadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlAcceso")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FacultadId");

                    b.ToTable("Facultad");
                });

            modelBuilder.Entity("Models.Foro", b =>
                {
                    b.Property<Guid>("ForoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ForoId");

                    b.ToTable("Foro");
                });

            modelBuilder.Entity("Models.Material", b =>
                {
                    b.Property<Guid>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CursoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MaterialId");

                    b.HasIndex("CursoId");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("Models.Mensaje", b =>
                {
                    b.Property<Guid>("MensajeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Contenido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EmisorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmisorId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("FechaDeEnviado")
                        .HasColumnType("datetime2");

                    b.HasKey("MensajeId");

                    b.HasIndex("EmisorId1");

                    b.ToTable("Mensaje");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Mensaje");
                });

            modelBuilder.Entity("Models.Respuesta", b =>
                {
                    b.Property<Guid>("RespuestaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AlumnoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AlumnoId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("EncuestaActividadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Mensaje")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RespuestaId");

                    b.HasIndex("AlumnoId1");

                    b.HasIndex("EncuestaActividadId");

                    b.ToTable("Respuesta");
                });

            modelBuilder.Entity("Models.TemaForo", b =>
                {
                    b.Property<Guid>("TemaForoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Asunto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EmisorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmisorId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("ForoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Mensaje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SubscripcionADiscusion")
                        .HasColumnType("bit");

                    b.HasKey("TemaForoId");

                    b.HasIndex("EmisorId1");

                    b.HasIndex("ForoId");

                    b.ToTable("TemaForo");
                });

            modelBuilder.Entity("Models.TemplateCurso", b =>
                {
                    b.Property<Guid>("TemplateCursoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TemplateCursoId");

                    b.ToTable("TemplateCurso");
                });

            modelBuilder.Entity("Models.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Apellidos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nombres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("UserName_udelar")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");
                });

            modelBuilder.Entity("Models.UsuarioCurso", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CursoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UsuarioId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UsuarioId", "CursoId");

                    b.HasIndex("CursoId");

                    b.HasIndex("UsuarioId1");

                    b.ToTable("UsuarioCurso");
                });

            modelBuilder.Entity("Models.UsuarioEnviaMensaje", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MensajeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UsuarioEnviaId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UsuarioId", "MensajeId");

                    b.HasIndex("MensajeId");

                    b.HasIndex("UsuarioEnviaId");

                    b.ToTable("UsuarioEnviaMensaje");
                });

            modelBuilder.Entity("Models.UsuarioRecibeMensaje", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MensajeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UsuarioRecibeId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UsuarioId", "MensajeId");

                    b.HasIndex("MensajeId");

                    b.HasIndex("UsuarioRecibeId");

                    b.ToTable("UsuarioRecibeMensaje");
                });

            modelBuilder.Entity("Models.ClaseDictada", b =>
                {
                    b.HasBaseType("Models.Actividad");

                    b.HasDiscriminator().HasValue("ClaseDictada");
                });

            modelBuilder.Entity("Models.Encuesta", b =>
                {
                    b.HasBaseType("Models.Actividad");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EsAdministrador")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Encuesta");
                });

            modelBuilder.Entity("Models.Trabajo", b =>
                {
                    b.HasBaseType("Models.Actividad");

                    b.Property<int>("Calificacion")
                        .HasColumnType("int");

                    b.Property<bool>("EsIndividual")
                        .HasColumnType("bit");

                    b.Property<string>("Nota")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Trabajo");
                });

            modelBuilder.Entity("Models.MensajeDirecto", b =>
                {
                    b.HasBaseType("Models.Mensaje");

                    b.Property<Guid>("ReceptorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReceptorId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("ReceptorId1");

                    b.HasDiscriminator().HasValue("MensajeDirecto");
                });

            modelBuilder.Entity("Models.MensajeTema", b =>
                {
                    b.HasBaseType("Models.Mensaje");

                    b.Property<bool>("MensajeBloqueado")
                        .HasColumnType("bit");

                    b.Property<Guid>("TemaForoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("TemaForoId");

                    b.HasDiscriminator().HasValue("MensajeTema");
                });

            modelBuilder.Entity("Models.Administrador", b =>
                {
                    b.HasBaseType("Models.Usuario");

                    b.HasDiscriminator().HasValue("Administrador");
                });

            modelBuilder.Entity("Models.AdministradorFacultad", b =>
                {
                    b.HasBaseType("Models.Usuario");

                    b.HasDiscriminator().HasValue("AdministradorFacultad");
                });

            modelBuilder.Entity("Models.Alumno", b =>
                {
                    b.HasBaseType("Models.Usuario");

                    b.HasDiscriminator().HasValue("Alumno");
                });

            modelBuilder.Entity("Models.Docente", b =>
                {
                    b.HasBaseType("Models.Usuario");

                    b.HasDiscriminator().HasValue("Docente");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Actividad", b =>
                {
                    b.HasOne("Models.Curso", "Curso")
                        .WithMany("ActividadLista")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.AdministradorFacultadFacultad", b =>
                {
                    b.HasOne("Models.Usuario", "AdministradorFacultad")
                        .WithMany()
                        .HasForeignKey("AdministradorFacultadId1");

                    b.HasOne("Models.Facultad", "Facultad")
                        .WithMany()
                        .HasForeignKey("FacultadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.AlumnoClaseDictada", b =>
                {
                    b.HasOne("Models.Usuario", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId1");

                    b.HasOne("Models.ClaseDictada", "ClaseDictada")
                        .WithMany()
                        .HasForeignKey("ClaseDictadaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.AlumnoCurso", b =>
                {
                    b.HasOne("Models.Usuario", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId1");

                    b.HasOne("Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.AlumnoTrabajo", b =>
                {
                    b.HasOne("Models.Usuario", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId1");

                    b.HasOne("Models.Trabajo", "Trabajo")
                        .WithMany()
                        .HasForeignKey("TrabajoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Carrera", b =>
                {
                    b.HasOne("Models.Facultad", "Facultad")
                        .WithMany()
                        .HasForeignKey("FacultadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.CarreraCurso", b =>
                {
                    b.HasOne("Models.Carrera", "Carrera")
                        .WithMany()
                        .HasForeignKey("CarreraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Comunicado", b =>
                {
                    b.HasOne("Models.Usuario", null)
                        .WithMany("ComunicadoLista")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Models.ComunicadoCurso", b =>
                {
                    b.HasOne("Models.Comunicado", "Comunicado")
                        .WithMany()
                        .HasForeignKey("ComunicadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.ComunicadoFacultad", b =>
                {
                    b.HasOne("Models.Comunicado", "Comunicado")
                        .WithMany()
                        .HasForeignKey("ComunicadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Facultad", "Facultad")
                        .WithMany()
                        .HasForeignKey("FacultadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Curso", b =>
                {
                    b.HasOne("Models.TemplateCurso", "TemplateCurso")
                        .WithMany()
                        .HasForeignKey("TemplateCursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.CursoForo", b =>
                {
                    b.HasOne("Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Foro", "Foro")
                        .WithMany()
                        .HasForeignKey("ForoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.DocenteTrabajo", b =>
                {
                    b.HasOne("Models.Docente", "Docente")
                        .WithMany()
                        .HasForeignKey("DocenteId1");

                    b.HasOne("Models.Trabajo", "Trabajo")
                        .WithMany()
                        .HasForeignKey("TrabajoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Material", b =>
                {
                    b.HasOne("Models.Curso", null)
                        .WithMany("MaterialLista")
                        .HasForeignKey("CursoId");
                });

            modelBuilder.Entity("Models.Mensaje", b =>
                {
                    b.HasOne("Models.Usuario", "Emisor")
                        .WithMany()
                        .HasForeignKey("EmisorId1");
                });

            modelBuilder.Entity("Models.Respuesta", b =>
                {
                    b.HasOne("Models.Usuario", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId1");

                    b.HasOne("Models.Encuesta", null)
                        .WithMany("RespuestaLista")
                        .HasForeignKey("EncuestaActividadId");
                });

            modelBuilder.Entity("Models.TemaForo", b =>
                {
                    b.HasOne("Models.Usuario", "Emisor")
                        .WithMany()
                        .HasForeignKey("EmisorId1");

                    b.HasOne("Models.Foro", null)
                        .WithMany("TemaForoLista")
                        .HasForeignKey("ForoId");
                });

            modelBuilder.Entity("Models.UsuarioCurso", b =>
                {
                    b.HasOne("Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId1");
                });

            modelBuilder.Entity("Models.UsuarioEnviaMensaje", b =>
                {
                    b.HasOne("Models.Mensaje", "Mensaje")
                        .WithMany()
                        .HasForeignKey("MensajeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Usuario", "UsuarioEnvia")
                        .WithMany()
                        .HasForeignKey("UsuarioEnviaId");
                });

            modelBuilder.Entity("Models.UsuarioRecibeMensaje", b =>
                {
                    b.HasOne("Models.Mensaje", "Mensaje")
                        .WithMany()
                        .HasForeignKey("MensajeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Usuario", "UsuarioRecibe")
                        .WithMany()
                        .HasForeignKey("UsuarioRecibeId");
                });

            modelBuilder.Entity("Models.MensajeDirecto", b =>
                {
                    b.HasOne("Models.Usuario", "Receptor")
                        .WithMany()
                        .HasForeignKey("ReceptorId1");
                });

            modelBuilder.Entity("Models.MensajeTema", b =>
                {
                    b.HasOne("Models.TemaForo", "TemaForo")
                        .WithMany()
                        .HasForeignKey("TemaForoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}