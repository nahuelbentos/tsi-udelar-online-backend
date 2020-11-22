using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;
using Models;

namespace Business.Carreras
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid CarreraId { get; set; }
            public String Nombre { get; set; }
            public String Descripcion { get; set; }
            public Guid FacultadId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(c => c.Nombre).NotEmpty().WithMessage("El Nombre es requerido");
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
                var carrera = await this.context.Carrera.FindAsync(request.CarreraId);

                if (carrera == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La carrera no existe"});
                
                //se realiza este control porque el request si puede traer la facultad vacia, quiere decir que no recibe cambios
                Facultad facultad = null;
                if (request.FacultadId != null)
                    facultad = await this.context.Facultad.FindAsync(request.FacultadId);

                carrera.Nombre = request.Nombre ?? carrera.Nombre;
                carrera.Descripcion = request.Descripcion ?? carrera.Descripcion;
                carrera.Facultad = facultad ?? carrera.Facultad;

                var res = await this.context.SaveChangesAsync();

                if (res > 0)
                    return Unit.Value;
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar la carrera" });

            }
        }
    }
}