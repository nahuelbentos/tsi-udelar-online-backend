using System.Net;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;
using Business.Datatypes;
using System.Collections.Generic;

namespace Business.Foros
{
  public class ConsultaById
  {
    public class Ejecuta : IRequest<DtForo>
    {
      public Guid ForoId { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta, DtForo>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<DtForo> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        // Hay que reemplazarlo con DataTypes
        var foro = await this.context.Foro
                                        .Include(f => f.TemaForoLista)
                                            .ThenInclude(tf => tf.Emisor)
                                        .Include(f => f.TemaForoLista)
                                            .ThenInclude(tf => tf.MensajeTemaLista)
                                                .ThenInclude(mt => mt.Emisor)
                                        .Where(f => f.ForoId == request.ForoId)
                                        .FirstOrDefaultAsync();
        if (foro == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un curso con el CursoId ingresado" });


        List<DtTemaForo> dtTemaForos = new List<DtTemaForo>();
        foreach (var temaForo in foro.TemaForoLista)
        {

          List<DtMensajeTema> dtMensajeTemas = new List<DtMensajeTema>();
          foreach (var mensajeTema in temaForo.MensajeTemaLista)
          {
            dtMensajeTemas.Add(new DtMensajeTema
            {
              Contenido = mensajeTema.Contenido,
              Emisor = mensajeTema.Emisor,
              EmisorId = mensajeTema.EmisorId,
              FechaDeEnviado = mensajeTema.FechaDeEnviado,
              MensajeBloqueado = mensajeTema.MensajeBloqueado,
              MensajeId = mensajeTema.MensajeId,
              TemaForo = mensajeTema.TemaForo,
              TemaForoId = mensajeTema.TemaForoId,
            });
          }

          dtMensajeTemas.Sort((x, y) => x.FechaDeEnviado.CompareTo(y.FechaDeEnviado));
          dtTemaForos.Add(new DtTemaForo
          {

            TemaForoId = temaForo.TemaForoId,
            ArchivoData = temaForo.ArchivoData,
            ArchivoExtension = temaForo.ArchivoExtension,
            ArchivoNombre = temaForo.ArchivoNombre,
            Asunto = temaForo.Asunto,
            Emisor = $"{temaForo.Emisor.Nombres} {temaForo.Emisor.Apellidos}",
            EmisorData = temaForo.Emisor,
            EmisorId = temaForo.EmisorId,
            FechaCreado = temaForo.FechaCreado,
            Foro = temaForo.Foro,
            ForoId = temaForo.ForoId,
            Mensaje = temaForo.Mensaje,
            SubscripcionADiscusion = temaForo.SubscripcionADiscusion,
            ListaMensajeTema = dtMensajeTemas
          });
        }

        return new DtForo
        {
          ForoId = foro.ForoId,
          Descripcion = foro.Descripcion,
          Titulo = foro.Titulo,
          TemaForoLista = dtTemaForos,
        };
      }
    }
  }
}