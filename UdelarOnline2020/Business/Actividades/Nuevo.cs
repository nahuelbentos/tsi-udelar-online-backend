using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.Actividades
{
  public class Nuevo
  {
    public class Ejecuta : IRequest
    {
      public DateTime FechaRealizada { get; set; }
      public DateTime FechaFinalizada { get; set; }
      public String Tipo { get; set; }
      public String Nombre { get; set; }
      public String Descripcion { get; set; }
      public bool EsAdministrador { get; set; }
      public bool EsIndividual { get; set; }
      public int Calificacion { get; set; }
      public String Nota { get; set; }

      public string ArchivoData { get; set; }
      public string ArchivoNombre { get; set; }
      public string ArchivoExtension { get; set; }

      //Restantes de PruebaOnline
      public DateTime Fecha { get; set; }
      public string Url { get; set; }
      public int MinutosExpiracion { get; set; }
      public bool Activa { get; set; }

      public string UsuarioId { get; set; }

    }
    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;
      private readonly ILogger<Manejador> logger;

      public Manejador(UdelarOnlineContext context, ILogger<Manejador> logger)
      {
        this.context = context;
        this.logger = logger;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {


        var usuario = await this.context.Usuario.FindAsync( request.UsuarioId );        

        if (usuario == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con ese Id." });

        Actividad actividad = null;
        byte[] archivoData = null;
        if (request.ArchivoData != null)
          archivoData = Convert.FromBase64String(request.ArchivoData);

        string archivoNombre = null;
        if (request.ArchivoNombre != null)
          archivoNombre = request.Nombre;

        string archivoExtension = null;
        if (request.ArchivoExtension != null)
          archivoExtension = request.ArchivoExtension;

        switch (request.Tipo)
        {
          case "ClaseDictada":
            actividad = new ClaseDictada
            {
              ArchivoData = archivoData,
              ArchivoExtension = archivoExtension,
              ArchivoNombre = archivoNombre,
            };
            break;
          case "Encuesta":
            actividad = new Encuesta
            {

              EsAdministrador = request.EsAdministrador,

            };
            break;
          case "Trabajo":
            actividad = new Trabajo
            {

              ArchivoData = archivoData,
              ArchivoExtension = archivoExtension,
              ArchivoNombre = archivoNombre,
              EsIndividual = request.EsIndividual,
              Calificacion = request.Calificacion,
              Nota = request.Nota

            };
            break;

          case "PruebaOnline":
            actividad = new PruebaOnline
            {
              Fecha = request.Fecha,
              Url = request.Url,
              MinutosExpiracion = request.MinutosExpiracion,
              Activa = request.Activa,
            };
            break;
          default:
            throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El tipo de actividad debe ser ClaseDictada, Encuesta, PruebaOnline o Trabajo" });
        }

        actividad.FechaFinalizada = request.FechaFinalizada;
        actividad.FechaRealizada = request.FechaRealizada;
        actividad.Nombre = request.Nombre;
        actividad.Descripcion = request.Descripcion;
        
        actividad.Usuario = usuario;
        actividad.UsuarioId = usuario.Id;
 
        this.context.Actividad.Add(actividad);

        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          return Unit.Value;
        }

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar la actividad" });
      }
    }
  }
}