using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Comunicados {
    public class PublicarFacultad {
        public class Ejecuta : IRequest {

            public Guid ComunicadoId { get; set; }

            //public Comunicado Comunicado { get; set; }

            public Guid FacultadId { get; set; }

            //public Facultad Facultad { get; set; }
            

        }

        public class EjecutaValidator : AbstractValidator<Ejecuta> {
            public EjecutaValidator () {
                RuleFor (c => c.ComunicadoId).NotEmpty ().WithMessage ("El Id del comunicado es requerido.");
                RuleFor (c => c.FacultadId).NotEmpty ().WithMessage ("El Id de la facultad es requerido.");
            }
        }

        public class Manejador : IRequestHandler<Ejecuta> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
                var comunicadoExiste = await this.context.Comunicado.Where(e => e.ComunicadoId == request.ComunicadoId).FirstOrDefaultAsync();
      
                if (comunicadoExiste == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el comunicado ingresado" });
                }

                var facultad = await this.context.Facultad.Where(e => e.FacultadId == request.FacultadId).FirstOrDefaultAsync();
      
                if (comunicadoExiste == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe la facultad ingresada" });
                }


                var comunicado = new ComunicadoFacultad {
                    ComunicadoId = Guid.NewGuid (),
                    Comunicado = comunicadoExiste,
                    FacultadId = request.FacultadId,
                    Facultad = facultad
                };

                this.context.ComunicadoFacultad.Add (comunicado);

                var res = await this.context.SaveChangesAsync ();
                if (res > 0) {
                    return Unit.Value;
                }

                throw new Exception ("No se pudo publicar el comunicado en la facultad");

            }
        }
    }
}