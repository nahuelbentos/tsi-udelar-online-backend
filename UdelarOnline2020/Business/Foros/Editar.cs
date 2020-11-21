using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.Foros
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid ForoId { get; set; }
            public String Titulo { get; set; }
            public String Descripcion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(f => f.ForoId).NotEmpty();
                RuleFor(f => f.Titulo).NotEmpty().WithMessage("El Titulo es requerido");
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
                var foro = await this.context.Foro.FindAsync(request.ForoId);

                if (foro == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El foro no existe" });
                
                foro.Titulo = request.Titulo ?? foro.Titulo;
                foro.Descripcion = request.Descripcion ?? foro.Descripcion;

                var res = await this.context.SaveChangesAsync();

                if (res > 0)
                    return Unit.Value;
                
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el foro" });
            }
        }
    }
}