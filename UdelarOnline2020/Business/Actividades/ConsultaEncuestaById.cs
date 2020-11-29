using System;
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

namespace Business.Actividades
{
  public class ConsultaEncuestaById
  {
    public class Ejecuta : IRequest<DtEncuesta>
    {
      public Guid Id { get; set; }

    }

    public class Manejador : IRequestHandler<Ejecuta, DtEncuesta>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<DtEncuesta> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var encuesta = await this.context.Encuesta
                                            .Include(e => e.Facultad)
                                            .Include(e => e.Usuario)
                                            .Include(e => e.PreguntaLista)
                                                .ThenInclude(p => p.RespuestaLista)
                                                    .ThenInclude(r => r.Alumno)
                                            .Where(a => a.ActividadId == request.Id).FirstOrDefaultAsync();


        if (encuesta == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe una encuesta con el Id ingresado" });


        return new DtEncuesta
        {
          ActividadId = encuesta.ActividadId,
          Descripcion = encuesta.Descripcion,
          EsAdministrador = encuesta.EsAdministrador,
          Facultad = encuesta.Facultad,
          FacultadId = encuesta.FacultadId,
          FechaFinalizada = encuesta.FechaFinalizada,
          FechaRealizada = encuesta.FechaRealizada,
          Nombre = encuesta.Nombre,
          Usuario = encuesta.Usuario,
          UsuarioId = encuesta.UsuarioId,
          PreguntaLista = encuesta.PreguntaLista,

        };




      }
    }
  }
}