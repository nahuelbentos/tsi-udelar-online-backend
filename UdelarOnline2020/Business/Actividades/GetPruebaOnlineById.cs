using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Business.Actividades
{
  public class GetPruebaOnlineById
  {
    public class Ejecuta : IRequest<DtPruebaOnline>
    {
      public Guid Id { get; set; }

    }


    public class Manejador : IRequestHandler<Ejecuta, DtPruebaOnline>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<DtPruebaOnline> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        //Hay que devolver Datatypes
        var pruebaOnline = await this.context.PruebaOnline
                                                        .Include(a => a.Usuario)
                                                        .Include(a => a.ListaPreguntaRespuesta)
                                                        .Where(a => a.ActividadId == request.Id)
                                                        .FirstOrDefaultAsync();

        return new DtPruebaOnline
        {
          Activa = pruebaOnline.Activa,
          ActividadId = pruebaOnline.ActividadId,
          Descripcion = pruebaOnline.Descripcion,
          Fecha = pruebaOnline.Fecha,
          FechaFinalizada = pruebaOnline.FechaFinalizada,
          FechaRealizada = pruebaOnline.FechaRealizada,
          ListaPreguntaRespuesta = pruebaOnline.ListaPreguntaRespuesta,
          MinutosExpiracion = pruebaOnline.MinutosExpiracion,
          Nombre = pruebaOnline.Nombre,
          Url = pruebaOnline.Url,
          Usuario = pruebaOnline.Usuario,
          UsuarioId = pruebaOnline.UsuarioId

        };
      }
    }
  }
}