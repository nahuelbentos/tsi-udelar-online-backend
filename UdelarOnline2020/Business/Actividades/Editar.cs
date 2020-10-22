using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Actividades
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid ActividadId { get; set; }
            public DateTime FechaRealizada { get; set; }
            public DateTime FechaFinalizada { get; set; }
            public String Tipo { get; set; }
            //public File Archivo { get; set; }
            public String Nombre { get; set; }
            public String Descripcion { get; set; }
            public bool EsAdministrador { get; set; }
            public bool EsIndividual { get; set; }
            public int Calificacion { get; set; }
            public String Nota { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(a => a.FechaRealizada).NotEmpty();
                RuleFor(a => a.FechaFinalizada).NotEmpty();
                RuleFor(a => a.Tipo).NotEmpty();
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
                var actividad = await this.context.Actividad.FindAsync(request.ActividadId);
                if (actividad == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe la actividad ingresada." });
                }

                var tipo = "Models." + request.Tipo;


                if (!tipo.Equals(actividad.GetType().ToString()))
                {
                    Actividad actividadActualizar = null;

                    switch (request.Tipo)
                    {
                        case "Trabajo":
                        actividadActualizar = new Trabajo();
                        break;
                        case "ClaseDictada":
                        actividadActualizar = new ClaseDictada();
                        break;
                        case "Encuesta":
                        actividadActualizar = new Encuesta();
                        break;
                        default:
                        throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El tipo de actividad debe ser Trabajo, ClaseDictada o Encuesta" });
                    }

                    actividadActualizar = await this.editarActividad(actividad, actividadActualizar, request);

                    this.context.Actividad.Add(actividadActualizar);

                    var resultado = await this.context.SaveChangesAsync();

                    if (resultado > 0)
                        return Unit.Value;
                }                                   
                else
                {

                actividad = await this.editarActividad(actividad, null, request);

                this.context.Actividad.Update(actividad);

                var resultado = await this.context.SaveChangesAsync();


                if (resultado > 0)
                    return Unit.Value; 
                }

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo editar la actividad" });
            }

            private async Task<Actividad> editarActividad(Actividad actividadOld, Actividad actividadActualizar, Ejecuta request)
            {    
                Actividad actividad = actividadActualizar ?? actividadOld;

                //No me deja usar operador ??
                actividad.FechaFinalizada = request.FechaFinalizada;
                actividad.FechaRealizada = request.FechaRealizada;

                switch (request.Tipo)
                {
                    case "ClaseDictada":
                        /*

                        ClaseDictada claseAux = (ClaseDictada)actividad;
                        ClaseDictada claseOldAux = (ClaseDictada)actividadOld;
                        claseAux.Archivo = request.Archivo ?? claseOldAux.Archivo;
                        
                        */
                        break;
                    case "Encuesta":
                        //no me permitia acceder al atributo nombre sin hacer conversion implicita, por eso está asi 22-10-2020 Albert
                        Encuesta encuestaAux = (Encuesta)actividad;
                        Encuesta encuestaOldAux = (Encuesta)actividadOld;
                        encuestaAux.Nombre = request.Nombre ?? encuestaOldAux.Nombre;
                        encuestaAux.Descripcion = request.Descripcion ?? encuestaOldAux.Descripcion;
                        //no me permite operador ?? con bool
                        encuestaAux.EsAdministrador = request.EsAdministrador;
                        break;
                    case "Trabajo":
                        Trabajo trabajoAux = (Trabajo)actividad;
                        Trabajo trabajoOldAux = (Trabajo)actividadOld;

                        trabajoAux.Nota = request.Nota ?? trabajoOldAux.Nota;
                        //no me permite usar operador ?? con bool
                        trabajoAux.EsIndividual = request.EsIndividual;
                        trabajoAux.Calificacion = request.Calificacion;
                        break;
                    default:
                        throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El tipo de actividad debe ser ClaseDictada, Encuesta o Trabajo" });
                }

                if (actividadActualizar != null)
                // Update
                {
                
                actividad.Curso = actividadOld.Curso;
                actividad.CursoId = actividadOld.CursoId;

                await this.eliminarActividad(actividadOld);
                }

                return actividad;

            }


            private Task eliminarActividad(Actividad actividad)
            {
                var res = this.context.Actividad.Remove(actividad);
                if (res.State != EntityState.Deleted)
                    throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrió un error al eliminar el usuario." });
                return Task.CompletedTask;
            }

        }
    }
}