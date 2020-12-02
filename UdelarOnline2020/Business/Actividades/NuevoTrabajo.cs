using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.Actividades
{
    public class NuevoTrabajo
    {
        public class Ejecuta : IRequest
        {
            public Guid ActividadId { get; set; }
            public string ArchivoData { get; set; }
            public string ArchivoNombre { get; set; }
            public string ArchivoExtension { get; set; }
            public string UsuarioId { get; set; }

        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
        public EjecutaValidator()
        {
            RuleFor(c => c.ActividadId).NotEmpty().WithMessage("El actividadId es requerido.");
            RuleFor(c => c.ArchivoData).NotEmpty().WithMessage("El ArchivoData es requerido.");
            RuleFor(c => c.UsuarioId).NotEmpty().WithMessage("El UsuarioId es requerido.");
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
                var trabajo = await this.context.Trabajo.FindAsync(request.ActividadId);
                if (trabajo == null)
                {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe la actividad ingresada." });
                }

                var alumno = await this.context.Alumno.FindAsync(request.UsuarioId);
                if (alumno == null)
                {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el alumno ingresado." });
                }

                byte[] archivoData = null;
                if(request.ArchivoData != null)
                    archivoData = Convert.FromBase64String(request.ArchivoData);

                string archivoNombre = null;
                if(request.ArchivoNombre != null)
                    archivoNombre = request.ArchivoNombre;

                string archivoExtension = null;
                if(request.ArchivoExtension != null)
                    archivoExtension = request.ArchivoExtension;

                trabajo.ArchivoNombre = archivoNombre ?? trabajo.ArchivoNombre;
                trabajo.ArchivoExtension = archivoExtension ?? trabajo.ArchivoExtension;
                trabajo.ArchivoData = archivoData ?? trabajo.ArchivoData;

                alumno.ActividadLista.Add(trabajo);

                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar el trabajo" });
            }
        }
    }
}