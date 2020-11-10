using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Models;

namespace Persistence
{
  public class UdelarOnlineContext : IdentityDbContext<Usuario>
  {
    public UdelarOnlineContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<AlumnoClaseDictada>().HasKey(alumnoClaseDictada => new { alumnoClaseDictada.AlumnoId, alumnoClaseDictada.ClaseDictadaId });
      modelBuilder.Entity<AlumnoCurso>().HasKey(alumnoCurso => new { alumnoCurso.AlumnoId, alumnoCurso.CursoId });
      modelBuilder.Entity<AlumnoTrabajo>().HasKey(alumnoTrabajo => new { alumnoTrabajo.AlumnoId, alumnoTrabajo.TrabajoId });
      modelBuilder.Entity<CarreraCurso>().HasKey(carreraCurso => new { carreraCurso.CarreraId, carreraCurso.CursoId });
      modelBuilder.Entity<ComunicadoCurso>().HasKey(comunicadoCurso => new { comunicadoCurso.ComunicadoId, comunicadoCurso.CursoId });
      modelBuilder.Entity<ComunicadoFacultad>().HasKey(comunicadoFacultad => new { comunicadoFacultad.ComunicadoId, comunicadoFacultad.FacultadId });

      modelBuilder.Entity<DocenteTrabajo>().HasKey(docenteTrabajo => new { docenteTrabajo.DocenteId, docenteTrabajo.TrabajoId });
      modelBuilder.Entity<UsuarioCurso>().HasKey(usuarioCurso => new { usuarioCurso.UsuarioId, usuarioCurso.CursoId });
      modelBuilder.Entity<UsuarioEnviaMensaje>().HasKey(usuarioEnviaMensaje => new { usuarioEnviaMensaje.UsuarioId, usuarioEnviaMensaje.MensajeId });
      modelBuilder.Entity<UsuarioRecibeMensaje>().HasKey(usuarioRecibeMensaje => new { usuarioRecibeMensaje.UsuarioId, usuarioRecibeMensaje.MensajeId });

      modelBuilder.Entity<TemplateCursoSeccion>().HasKey(templateCursoSeccion => new { templateCursoSeccion.TemplateCursoId, templateCursoSeccion.SeccionId });

      modelBuilder.Entity<CursoSeccion>().HasKey(cursoSeccion => new { cursoSeccion.CursoId, cursoSeccion.SeccionId });

      modelBuilder.Entity<CursoSeccionMaterial>().HasKey(cursoSeccionMaterial => new { cursoSeccionMaterial.CursoId, cursoSeccionMaterial.SeccionId, cursoSeccionMaterial.MaterialId });
      modelBuilder.Entity<CursoSeccionActividad>().HasKey(cursoSeccionActividad => new { cursoSeccionActividad.CursoId, cursoSeccionActividad.SeccionId, cursoSeccionActividad.ActividadId });
      modelBuilder.Entity<CursoSeccionForo>().HasKey(cursoSeccionForo => new { cursoSeccionForo.CursoId, cursoSeccionForo.SeccionId, cursoSeccionForo.ForoId });


    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }


    public DbSet<Actividad> Actividad { get; set; }
    public DbSet<Administrador> Administrador { get; set; }
    public DbSet<AdministradorFacultad> AdministradorFacultad { get; set; }
    public DbSet<Alumno> Alumno { get; set; }
    public DbSet<AlumnoClaseDictada> AlumnoClaseDictada { get; set; }
    public DbSet<AlumnoCurso> AlumnoCurso { get; set; }
    public DbSet<AlumnoTrabajo> AlumnoTrabajo { get; set; }
    public DbSet<Carrera> Carrera { get; set; }
    public DbSet<CarreraCurso> CarreraCurso { get; set; }
    public DbSet<ClaseDictada> ClaseDictada { get; set; }
    public DbSet<Comunicado> Comunicado { get; set; }
    public DbSet<ComunicadoCurso> ComunicadoCurso { get; set; }
    public DbSet<ComunicadoFacultad> ComunicadoFacultad { get; set; }
    public DbSet<Curso> Curso { get; set; }
    public DbSet<Docente> Docente { get; set; }
    public DbSet<DocenteTrabajo> DocenteTrabajo { get; set; }
    public DbSet<Encuesta> Encuesta { get; set; }
    public DbSet<Facultad> Facultad { get; set; }
    public DbSet<Foro> Foro { get; set; }
    public DbSet<Material> Material { get; set; }
    public DbSet<Mensaje> Mensaje { get; set; }
    public DbSet<MensajeDirecto> MensajeDirecto { get; set; }
    public DbSet<MensajeTema> MensajeTema { get; set; }
    public DbSet<Respuesta> Respuesta { get; set; }
    public DbSet<TemaForo> TemaForo { get; set; }
    public DbSet<TemplateCurso> TemplateCurso { get; set; }
    public DbSet<Trabajo> Trabajo { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<UsuarioCurso> UsuarioCurso { get; set; }
    public DbSet<UsuarioEnviaMensaje> UsuarioEnviaMensaje { get; set; }
    public DbSet<UsuarioRecibeMensaje> UsuarioRecibeMensaje { get; set; }


    public DbSet<TemplateCursoSeccion> TemplateCursoSeccion { get; set; }
    public DbSet<Seccion> Seccion { get; set; }
    public DbSet<CursoSeccion> CursoSeccion { get; set; }
    public DbSet<CursoSeccionForo> CursoSeccionForo { get; set; }
    public DbSet<CursoSeccionMaterial> CursoSeccionMaterial { get; set; }
    public DbSet<CursoSeccionActividad> CursoSeccionActividad { get; set; }

  }
}
