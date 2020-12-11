using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Foros
{
  public class Consulta
  {
    public class Ejecuta : IRequest<List<DtForo>> { }

    public class Manejador : IRequestHandler<Ejecuta, List<DtForo>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<DtForo>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        Console.WriteLine("consulto los foros");
        //Hay que devolver DataTypes
        var foros = await this.context.Foro
                                        .Include(f => f.TemaForoLista)
                                          .ThenInclude(tf => tf.Emisor)
                                        .Include(f => f.TemaForoLista)
                                          .ThenInclude(tf => tf.MensajeTemaLista)
                                        .ToListAsync();

        List<DtForo> dtForos = new List<DtForo>();
        foreach (var foro in foros)
        {
          List<DtTemaForo> dtTemaForos = new List<DtTemaForo>();
          foreach (var temaForo in foro.TemaForoLista)
          {

            List<DtMensajeTema> dtMensajeTemas = new List<DtMensajeTema>();
            if(temaForo.MensajeTemaLista.Count > 0){
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

            }



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

          dtForos.Add(new DtForo
          {
            ForoId = foro.ForoId,
            Descripcion = foro.Descripcion,
            Titulo = foro.Titulo,
            TemaForoLista = dtTemaForos,
          });
        }

        return dtForos;
      }
    }
  }
}