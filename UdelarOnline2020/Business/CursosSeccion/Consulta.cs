using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.CursosSeccion
{
  public class Consulta
  {
    public class Ejecuta : IRequest<List<DtCursoSeccion>> { }
    public class Manejador : IRequestHandler<Ejecuta, List<DtCursoSeccion>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<DtCursoSeccion>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        List<DtCursoSeccion> dtCursoSecciones = new List<DtCursoSeccion>();
        var cursos = await this.context.CursoSeccion
                                      .Include("Curso")
                                      .Include("Seccion")
                                      .ToListAsync();
        if(cursos.Count > 0){
           foreach( var c in cursos){
             DtCursoSeccion dt = new DtCursoSeccion {
              CursoData = c.Curso,
              CursoId = c.Curso.CursoId,
              Curso = c.Curso.Nombre,
              SeccionData = c.Seccion,
              SeccionId = c.SeccionId,
              Seccion = c.Seccion.Nombre,
              ActividadLista = null,
              ForoLista = null,
              MaterialLista = null,
            };
            dtCursoSecciones.Add(dt);
          }          
        }
         
        return dtCursoSecciones;
      }
    }


  }
}