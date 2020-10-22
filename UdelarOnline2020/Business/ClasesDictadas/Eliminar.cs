using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.ClasesDictadas
{
    public class Eliminar
    {
         public class Ejecuta : IRequest
    {
      public Guid ClaseDictadaId { get; set; }
    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(t => t.ClaseDictadaId).NotEmpty().WithMessage("Es necesario el ClaseDictadaId para eliminar un template de curso.");
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
        var claseDictada = await this.context.ClaseDictada.FindAsync(request.ClaseDictadaId);

        if (claseDictada == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La clase dictada no existe. " });

        this.context.ClaseDictada.Remove(claseDictada);

        var res = await this.context.SaveChangesAsync();

        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar la clase dictada" });
      }
    }
    }
}