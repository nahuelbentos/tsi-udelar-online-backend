using System;
using System.Collections.Generic;
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

namespace Business.TemplatesCursoSeccion {
    public class Editar {

        public class Ejecuta : IRequest {           
            public Guid TemplateCursoId { get; set; }
            public TemplateCurso TemplateCurso { get; set; }
            public List<Guid> Secciones { get; set; }

            }


        public class Manejador : IRequestHandler<Ejecuta> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {

                    var templateCurso = await this.context.TemplateCurso.FindAsync(request.TemplateCursoId);

                    if (templateCurso == null)
                    {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El template de curso no existe" });

                    }

                    templateCurso.Nombre = request.TemplateCurso.Nombre ?? templateCurso.Nombre;
                    templateCurso.Descripcion = request.TemplateCurso.Descripcion ?? templateCurso.Descripcion;

                    var res = await this.context.SaveChangesAsync();

                    if (res > 0){

                        var seccionesEliminar = await this.context.TemplateCursoSeccion.Where(t => t.TemplateCursoId == templateCurso.TemplateCursoId).ToListAsync();
                        this.context.RemoveRange(seccionesEliminar);

                        foreach (var seccion in request.Secciones)
                        {
                            var tcs= await this.context.TemplateCursoSeccion
                                                        .Where( tcs => tcs.TemplateCursoId == templateCurso.TemplateCursoId && tcs.SeccionId == seccion)
                                                        .FirstOrDefaultAsync();
                            if( tcs == null)   {
                                var seccionData = await this.context.Seccion.FindAsync(seccion);
                                var templateCursoSecion = new TemplateCursoSeccion{
                                    Seccion = seccionData,
                                    SeccionId = seccionData.SeccionId,
                                    TemplateCurso = templateCurso,
                                    TemplateCursoId = templateCurso.TemplateCursoId
                                };

                                this.context.TemplateCursoSeccion.Add(templateCursoSecion);
                            } 
                        }

                        var response = await this.context.SaveChangesAsync();
                        if( response > 0)
                            return Unit.Value;
                    }

                throw new ManejadorExcepcion (HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el temaplate de curso" });

            }
        }
    }
}