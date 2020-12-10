using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Cursos
{
  public class Consulta
  {
    public class Ejecuta : IRequest<List<DtCurso>> { }
    public class Manejador : IRequestHandler<Ejecuta, List<DtCurso>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<DtCurso>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
        var cursos = await this.context.Curso
                                      .Include(c => c.TemplateCurso)
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