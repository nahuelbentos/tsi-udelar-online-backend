using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Models;
using Persistence;

namespace Business.Facultades
{
  public class Nuevo
  {
    public class Ejecuta : IRequest
    {

      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public string UrlAcceso { get; set; }

    }

    public class EjecutaValidador : AbstractValidator<Ejecuta>
    {
      public EjecutaValidador()
      {
        RuleFor(f => f.Nombre).NotEmpty();
        RuleFor(f => f.Descripcion).NotEmpty();
        RuleFor(f => f.UrlAcceso).NotEmpty();
      }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var facultad = new Facultad
        {
          FacultadId = Guid.NewGuid(),
          Nombre = request.Nombre,
          Descripcion = request.Descripcion,
          UrlAcceso = request.UrlAcceso
        };

        await this.context.Facultad.AddAsync(facultad);

        var res = await this.context.SaveChangesAsync();


        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo insertar la facultad" });
      }
    }
  }
}