using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Business.AlumnoCursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid AlumnoId { get; set; }
            public Guid CursoId { get; set; }

        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(ac => ac.AlumnoId).NotEmpty().WithMessage("Es necesario AlumnoId para eliminar la inscripcion.");
                RuleFor(ac => ac.CursoId).NotEmpty().WithMessage("Es necesario CursoId para eliminar la inscripcion.");
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
                var ac = await this.context.AlumnoCurso.Where(ac => ac.AlumnoId == request.AlumnoId && ac.CursoId == request.CursoId).FirstOrDefaultAsync();

                if (ac == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La inscripcion no existe. " });

                this.context.AlumnoCurso.Remove(ac);

                var res = await this.context.SaveChangesAsync();

                if (res > 0)
                    return Unit.Value;

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar la inscripcion." });
            }
        }
    }
}