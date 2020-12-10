using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Actividades
{
    public class ConsultaPruebaOnline
    {
         public class Ejecuta : IRequest<List<DtPruebaOnline>> {
             public string UsuarioId { get; set; }
             
             
          }
        public class Manejador : IRequestHandler<Ejecuta, List<DtPruebaOnline>>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<DtPruebaOnline>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Hay que devolver Datatypes
                var pruebasOnlines = await this.context.PruebaOnline
                                                                .Include(a => a.Usuario)
                                                                .Include(a => a.ListaPreguntaRespuesta)
                                                                .Where(p => p.UsuarioId == request.UsuarioId)
                                                                .ToListAsync();
                
                List<DtPruebaOnline> dtPruebaOnlines = new List<DtPruebaOnline>();
                foreach (var pruebaOnline in pruebasOnlines)
                {   
                    
                    dtPruebaOnlines.Add(new DtPruebaOnline{
                        Activa = pruebaOnline.Activa,
                        ActividadId = pruebaOnline.ActividadId,
                        Descripcion = pruebaOnline.Descripcion,
                        Fecha = pruebaOnline.Fecha,
                        FechaFinalizada = pruebaOnline.FechaFinalizada,
                        FechaRealizada = pruebaOnline.FechaRealizada,
                        ListaPreguntaRespuesta = pruebaOnline.ListaPreguntaRespuesta,
                        MinutosExpiracion = pruebaOnline.MinutosExpiracion,
                        Nombre = pruebaOnline.Nombre,
                        Url = pruebaOnline.Url,
                        Usuario = pruebaOnline.Usuario,
                        UsuarioId = pruebaOnline.UsuarioId                        

                    });
                }    
                return dtPruebaOnlines;                                                        
            }
        }
    }
}