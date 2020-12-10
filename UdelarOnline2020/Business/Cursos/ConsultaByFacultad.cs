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
  public class ConsultaByFacultad
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
        var facultad = await this.context.Facultad.Include(f => f.CarreraLista).Where(f => f.FacultadId == request.Id).FirstOrDefaultAsync();

        if (facultad == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe una facultad con el Id ingresado" });
          
        List<Curso> cursos = new List<Curso>();
        foreach (var carrera in facultad.CarreraLista)
        {
          var auxCursos = await this.context.CarreraCurso
                                              .Where(cc => cc.CarreraId == carrera.CarreraId)
                                              .Select(c => c.Curso)
                                              .ToListAsync();
          cursos = cursos.Concat(auxCursos).ToList();
        }

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