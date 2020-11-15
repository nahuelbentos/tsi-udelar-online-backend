using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Models;
using Persistence;

namespace Business.Carreras
{
    public class AgregarCurso
    {
        public class Ejecuta : IRequest
        {
            public Guid CarreraId { get; set; }
            public Guid CursoId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(c => c.CarreraId).NotEmpty().WithMessage("CarreraId es requerido");
                RuleFor(c => c.CursoId).NotEmpty().WithMessage("CarreraId es requerido");
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
                
                var curso = await this.context.Curso.FindAsync(request.CursoId);
                if (curso == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La curso no existe"});
                
                CarreraCurso cc = new CarreraCurso 
                {
                    CarreraId = request.CarreraId,
                    Carrera = carrera,
                    CursoId = request.CursoId,
                    Curso = curso
                };

                var res = await this.context.SaveChangesAsync();

                if (res > 0)
                    return Unit.Value;
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar la carrera" });

            }
        }
    }
}