using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.Materiales
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
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
                byte[] archivoData = null;
                if(request.ArchivoData != null)
                    archivoData = Convert.FromBase64String(request.ArchivoData);

                string archivoNombre = null;
                if(request.ArchivoNombre != null)
                    archivoNombre = request.Nombre;

                string archivoExtension = null;
                if(request.ArchivoExtension != null)
                    archivoExtension = request.ArchivoExtension;

                
                var material = new Material {
                    ArchivoData = archivoData,
                    ArchivoExtension = archivoExtension,
                    ArchivoNombre = archivoNombre,
                    Descripcion = request.Descripcion,
                    Nombre = request.Nombre
                };
                
                context.Material.Add(material);
                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar el material" });
            }
        }
    }
}