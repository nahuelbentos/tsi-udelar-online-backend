using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Actividades
{
  public class Consulta
  {
    public class Ejecuta : IRequest<List<DtActividad>> { }
    public class Manejador : IRequestHandler<Ejecuta, List<DtActividad>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<DtActividad>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        //Hay que devolver Datatypes
        var actividades = await this.context.Actividad.Include(a => a.Usuario).ToListAsync();

        List<DtActividad> dtActividades = new List<DtActividad>();
        foreach (var actividada in actividades)
        {
          dtActividades.Add(new DtActividad
          {
            ActividadId = actividada.ActividadId,
            Descripcion = actividada.Descripcion,
            FechaFinalizada = actividada.FechaFinalizada,
            FechaRealizada = actividada.FechaRealizada,
            Nombre = actividada.Nombre,
            Usuario = actividada.Usuario,
            UsuarioId = actividada.UsuarioId,
            Tipo = actividada.GetType().ToString().Split('.')[1]
          });
        }
        return dtActividades;
      }
    }
  }
}