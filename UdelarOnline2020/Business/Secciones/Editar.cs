using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.Secciones
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid SeccionId { get; set; }
            public string Nombre { get; set; }

            public string Descripcion { get; set; }

            public bool IsDefault { get; set; }
            public bool IsVisible { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(s => s.Nombre).NotEmpty().WithMessage("El Nombre es requerido");
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
                var seccion = await this.context.Seccion.FindAsync(request.SeccionId);

                if (seccion == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La seccion no existe"});

                seccion.Nombre = request.Nombre ?? seccion.Nombre;
                seccion.Descripcion = request.Descripcion ?? seccion.Descripcion;
                //no hay operacion ?? para boolean
                seccion.IsDefault = request.IsDefault;
                seccion.IsVisible = request.IsVisible;

                var res = await this.context.SaveChangesAsync();

                if (res > 0)
                    return Unit.Value;
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar la seccion" });

            }
        }
    }
}