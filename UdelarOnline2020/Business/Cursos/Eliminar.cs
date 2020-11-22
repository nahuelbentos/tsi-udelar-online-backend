using System.Net;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistence;
using Business.ManejadorError;

namespace Business.Cursos
{
  public class Eliminar
  {
    public class Ejecuta : IRequest
    {
      public Guid CursoId { get; set; }
    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(c => c.CursoId).NotEmpty().WithMessage("Es necesario el CursoId para eliminar un curso.");
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
        var curso = await this.context.Curso.FindAsync(request.CursoId);

        if (curso == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El curso no existe. " });

        this.context.Curso.Remove(curso);

        var res = await this.context.SaveChangesAsync();

        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar el curso" });
      }
    }
  }
}