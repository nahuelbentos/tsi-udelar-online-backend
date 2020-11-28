using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Cursos
{
  public class ConsultaByFacultad
  {
    public class Ejecuta : IRequest<List<Curso>>
    {
      public Guid Id { get; set; }

    }

    public class Manejador : IRequestHandler<Ejecuta, List<Curso>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<Curso>> Handle(Ejecuta request, CancellationToken cancellationToken)
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

        return cursos;


      }
    }
  
  }
}