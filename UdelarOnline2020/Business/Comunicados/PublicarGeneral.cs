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
    public class PublicarGeneral {
        public class Ejecuta : IRequest {
            //public string Nombre { get; set; }
            //public string Descripcion { get; set; }
            //public string Url { get; set; }
            public Guid ComunicadoId { get; set; }

        }

        public class EjecutaValidator : AbstractValidator<Ejecuta> {
            public EjecutaValidator () {
                RuleFor (t => t.ComunicadoId).NotEmpty ().WithMessage ("El ComunicadoId es requerido.");
            }
        }

        public class Manejador : IRequestHandler<Ejecuta> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
                var comunicado = await this.context.Comunicado.Where(e => e.ComunicadoId == request.ComunicadoId).FirstOrDefaultAsync();
      
                if (comunicado == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el comunicado ingresado" });
                }
                
                var facultades = this.context.Facultad.ToList();
                foreach(var facultad in facultades){
                    var comunicadoFacultad = new ComunicadoFacultad {
                        ComunicadoId = comunicado.ComunicadoId,
                        Comunicado = comunicado,
                        FacultadId = facultad.FacultadId,
                        Facultad = facultad
                    };
                    await this.context.ComunicadoFacultad.AddAsync(comunicadoFacultad);
                }
                //this.context.Comunicado.Add (comunicado);

                var res = await this.context.SaveChangesAsync ();
                if (res > 0) {
                    return Unit.Value;
                }

                throw new Exception ("No se pudo dar de alta el comunicado");

            }
        }
    }
}