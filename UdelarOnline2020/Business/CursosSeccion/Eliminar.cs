using System.Net;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistence;
using Business.ManejadorError;

namespace Business.CursosSeccion
{
  public class Eliminar
  {
    public class Ejecuta : IRequest
    {
      public Guid CursoSeccionId { get; set; }
    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(c => c.CursoSeccionId).NotEmpty().WithMessage("Es necesario el CursoId para eliminar un curso.");
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
        var cursoSeccion = await this.context.CursoSeccion.FindAsync(request.CursoSeccionId);

        if (cursoSeccion == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El curso no existe. " });

        this.context.CursoSeccion.Remove(cursoSeccion);

        var res = await this.context.SaveChangesAsync();

        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar el curso" });
      }
    }
  }
}