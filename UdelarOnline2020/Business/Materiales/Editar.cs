using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Business.Materiales
{
  public class Editar
  {
    public class Ejecuta : IRequest
    {
      public Guid MaterialId { get; set; }

      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public string ArchivoData { get; set; }
      public string ArchivoNombre { get; set; }
      public string ArchivoExtension { get; set; }

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {


        RuleFor(c => c.Nombre).NotEmpty().WithMessage("El Nombre es requerido.");
        RuleFor(c => c.Descripcion).NotEmpty().WithMessage("El Descripcion es requerido.");

      }
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
        var material = await this.context.Material.FindAsync(request.MaterialId);
        if (material == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No se encontro el material" });
        }

        material.ArchivoData = Convert.FromBase64String(request.ArchivoData) ?? material.ArchivoData;
        material.ArchivoExtension = request.ArchivoExtension ?? material.ArchivoExtension;
        material.ArchivoNombre = request.ArchivoNombre ?? material.ArchivoNombre;
        material.Nombre = request.Nombre ?? material.Nombre;
        material.Descripcion = request.Descripcion ?? material.Descripcion;

        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          return Unit.Value;
        }
        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el material" });
      }
    }
  }
}