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

namespace Business.TemplatesCursoSeccion
{
  public class Editar
  {

    public class Ejecuta : IRequest
    {
      public Guid TemplateCursoId { get; set; }
      public TemplateCurso TemplateCurso { get; set; }
      public List<Guid> Secciones { get; set; }

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

        var templateCurso = await this.context.TemplateCurso.FindAsync(request.TemplateCursoId);

        Console.WriteLine("00 ");
        if (templateCurso == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El template de curso no existe" });

        }
        Console.WriteLine("01 ");
        var oldNombre = templateCurso.Nombre;
        var oldDescripcion = templateCurso.Descripcion;
        templateCurso.Nombre = request.TemplateCurso.Nombre ?? templateCurso.Nombre;
        templateCurso.Descripcion = request.TemplateCurso.Descripcion ?? templateCurso.Descripcion;

        Console.WriteLine("02 ");
        var res = await this.context.SaveChangesAsync();

        Console.WriteLine("03 res:  " + res);
        if (res > 0 || (oldDescripcion == request.TemplateCurso.Descripcion && oldNombre == request.TemplateCurso.Nombre))
        {
          Console.WriteLine("0 ");
          var seccionesEliminar = await this.context.TemplateCursoSeccion.Where(t => t.TemplateCursoId == templateCurso.TemplateCursoId).ToListAsync();
          bool continuar = true;
          if (seccionesEliminar.Count > 0)
          {

            Console.WriteLine("1 ");
            this.context.RemoveRange(seccionesEliminar);
            Console.WriteLine("2");

            var result = await this.context.SaveChangesAsync();
            Console.WriteLine("3 result: " + result);
            if (result <= 0) continuar = false;
          }


          if (continuar)
          {

            foreach (var seccion in request.Secciones)
            {
              var tcs = await this.context.TemplateCursoSeccion
                                          .Where(tcs => tcs.TemplateCursoId == templateCurso.TemplateCursoId && tcs.SeccionId == seccion)
                                          .FirstOrDefaultAsync();
              if (tcs == null)
              {
                var seccionData = await this.context.Seccion.FindAsync(seccion);
                var templateCursoSecion = new TemplateCursoSeccion
                {
                  Seccion = seccionData,
                  SeccionId = seccionData.SeccionId,
                  TemplateCurso = templateCurso,
                  TemplateCursoId = templateCurso.TemplateCursoId
                };

                this.context.TemplateCursoSeccion.Add(templateCursoSecion);
              }
            }

            var response = await this.context.SaveChangesAsync();
            if (response > 0)
              return Unit.Value;
          }

        }

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el temaplate de curso" });

      }
    }
  }
}