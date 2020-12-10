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

namespace Business.Cursos
{
    public class ConsultaByCarrera
  {
    public class Ejecuta : IRequest<List<DtCurso>>
    {
      public Guid Id { get; set; }

    }

    public class Manejador : IRequestHandler<Ejecuta, List<DtCurso>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<DtCurso>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var carrera = await this.context.Carrera.FindAsync(request.Id);
        if (carrera == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un carrera con ese Id." });

        var cursos = await this.context.CarreraCurso.Include(uc => uc.Curso)
                                                    .Where(u => u.CarreraId == request.Id)
                                                    .Select(c => c.Curso)
                                                    .ToListAsync();


        List<DtCurso> dtCursos = new List<DtCurso>();

        foreach (var curso in cursos)
        {
          var dtCurso = new DtCurso
          {
            CursoId = curso.CursoId,
            Descripcion = curso.Descripcion,
            ModalidadId = curso.Modalidad,
            Modalidad = Enum.GetName(typeof(ModalidadEnum), curso.Modalidad),
            Nombre = curso.Nombre,
            RequiereMatriculacion = curso.RequiereMatriculacion,
            SalaVirtual = curso.SalaVirtual,
            ZoomId = curso.ZoomId,
            ZoomPassword = curso.ZoomPassword,
            TemplateCurso = curso.TemplateCurso,
            TemplateCursoId = curso.TemplateCursoId,
            ActaCerrada = curso.ActaCerrada,

          };

          dtCursos.Add(dtCurso);

        }

        return dtCursos;


      }
    }


  }
}