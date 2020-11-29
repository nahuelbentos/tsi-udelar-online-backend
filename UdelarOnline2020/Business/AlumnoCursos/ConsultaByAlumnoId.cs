using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.AlumnoCursos
{
  public class ConsultaByAlumnoId
  {
    public class Ejecuta : IRequest<List<DtAlumnoCurso>>
    {
      public Guid AlumnoId { get; set; }

    }

    public class Manejador : IRequestHandler<Ejecuta, List<DtAlumnoCurso>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<DtAlumnoCurso>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var alumnoCursos = await this.context.AlumnoCurso.Include(ac => ac.Alumno).Include(ac => ac.Curso).Where(ac => ac.AlumnoId == request.AlumnoId).ToListAsync();
        if (!alumnoCursos.Any())
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No se encontraron inscripciones asociadas al Alumno" });

        List<DtAlumnoCurso> dtAlumnoCursos = new List<DtAlumnoCurso>();

        foreach (var ac in alumnoCursos)
        {
          DtAlumnoCurso dtAlumnoCurso = new DtAlumnoCurso
          {
            Alumno = $" {ac.Alumno.Nombres}  {ac.Alumno.Apellidos}",
            AlumnoId = ac.AlumnoId,
            Calificacion = ac.Calificacion,
            Curso = $" {ac.Curso.Nombre} - {ac.Curso.Descripcion}",
            CursoId = ac.CursoId,
            DataAlumno = ac.Alumno,
            DataCurso = ac.Curso,
            FechaActaCerrada = ac.FechaActaCerrada,
            Feedback = ac.Feedback,
            Inscripto = ac.Inscripto
          };

          dtAlumnoCursos.Add(dtAlumnoCurso);
        }
        return dtAlumnoCursos;
      }
    }
  }
}