using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.Respuestas
{
    public class Nuevo
    {
        
        public class Ejecuta : IRequest
        {

            public string Mensaje { get; set; }
            public Usuario Alumno { get; set; }

        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
        public EjecutaValidator()
        {
            RuleFor(c => c.Mensaje).NotEmpty().WithMessage("El Mensaje es requerido.");
            RuleFor(c => c.Alumno).NotEmpty().WithMessage("El Alumno es Requerido");
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
                var alumno = await this.context.Alumno.FindAsync(request.Alumno.Id);
                //var alumno = await this.context.Alumno.Where(a => a.UserName == request.Alumno.UserName).FirstOrDefault();
                if (alumno == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el alumno ingresado" });
                }
                var resp = new Respuesta {
                    Mensaje = request.Mensaje,
                    Alumno = request.Alumno
                };
                
                context.Respuesta.Add(resp);
                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar la respuesta" });
            }
        }
    }
}