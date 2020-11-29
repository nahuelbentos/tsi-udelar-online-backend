using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Materiales
{
  public class NuevoEnCursoSeccion
  {
    public class Ejecuta : IRequest
    {

      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public string ArchivoData { get; set; }
      public string ArchivoNombre { get; set; }
      public string ArchivoExtension { get; set; }

      public Guid CursoId { get; set; }
      public Guid SeccionId { get; set; }
      
      
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
        var curso = await this.context.Curso
                                        .Where(c => c.CursoId == request.CursoId)
                                        .FirstOrDefaultAsync();
        if (curso == null)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el curso ingresado" });
      
        var seccion = await this.context.Seccion
                                        .Where(c => c.SeccionId == request.SeccionId)
                                        .FirstOrDefaultAsync();
        if (seccion == null)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe la seccion ingresado" });
        
        var cursoSeccion = await this.context.CursoSeccion
                                        .Where( cs => cs.CursoId == curso.CursoId && cs.SeccionId == seccion.SeccionId)
                                        .FirstOrDefaultAsync();

        if( cursoSeccion == null)
            cursoSeccion = new CursoSeccion {
                Curso = curso,
                CursoId = curso.CursoId,
                Seccion = seccion,
                SeccionId = seccion.SeccionId
            };


        byte[] archivoData = null;
        if (request.ArchivoData != null)
          archivoData = Convert.FromBase64String(request.ArchivoData);

        string archivoNombre = null;
        if (request.ArchivoNombre != null)
          archivoNombre = request.Nombre;

        string archivoExtension = null;
        if (request.ArchivoExtension != null)
          archivoExtension = request.ArchivoExtension;


        var material = new Material
        {
          MaterialId = Guid.NewGuid(),
          ArchivoData = archivoData,
          ArchivoExtension = archivoExtension,
          ArchivoNombre = archivoNombre,
          Descripcion = request.Descripcion,
          Nombre = request.Nombre
        };

        this.context.Material.Add(material);
        
        if (await this.context.SaveChangesAsync() > 0)
        {
            CursoSeccionMaterial cursoSeccionMaterial = new CursoSeccionMaterial {

              Curso = curso,
              CursoId = curso.CursoId,
              Seccion = seccion,
              SeccionId = seccion.SeccionId,
              Material = material,
              MaterialId = material.MaterialId


            };
          this.context.CursoSeccionMaterial.Add( cursoSeccionMaterial );
 
          if (await this.context.SaveChangesAsync() > 0)
            return Unit.Value;
        }

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar el material en el Curso y Sección" });
      }
    }
  }
}