using System.Net.NetworkInformation;
using System.Net;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.Cursos
{
  public class Nuevo
  {
    public class Ejecuta : IRequest
    {
      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public ModalidadEnum ModalidadId { get; set; }
      public bool RequiereMatriculacion { get; set; }
      public string SalaVirtual { get; set; }
      public string ZoomId { get; set; }
      public string ZoomPassword { get; set; }
      public Guid? TemplateCursoId { get; set; }

    }



    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;
      private readonly ILogger<Manejador> logger;

      public Manejador(UdelarOnlineContext context, ILogger<Manejador> logger)
      {
        this.context = context;
        this.logger = logger;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {


        TemplateCurso templateCurso = null;

        if (request.TemplateCursoId != null)
        {

          templateCurso = await this.context.TemplateCurso.Where(tc => tc.TemplateCursoId == request.TemplateCursoId).FirstOrDefaultAsync();

          if (templateCurso == null)
            throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el template ingresado" });

        }

        var curso = new Curso
        {
          CursoId = Guid.NewGuid(),
          Descripcion = request.Descripcion,
          Nombre = request.Nombre,
          Modalidad = request.ModalidadId,
          RequiereMatriculacion = request.RequiereMatriculacion,
          SalaVirtual = request.SalaVirtual,
          ZoomId= request.ZoomId,
          ZoomPassword = request.ZoomPassword,
          TemplateCurso = templateCurso,
          ActaCerrada = false
        };

        this.context.Curso.Add(curso);

        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          if (templateCurso != null)
          {
            var secciones = await this.context.TemplateCursoSeccion
                                                .Include(tcs => tcs.Seccion)
                                                .Include(tcs => tcs.TemplateCurso)
                                                .Where(tcs => tcs.TemplateCursoId == templateCurso.TemplateCursoId)
                                                .Select(tcs => tcs.Seccion)
                                                .ToListAsync();
            if (secciones.Count > 0)
            {
              foreach (var seccion in secciones)
              {
                this.context.CursoSeccion.Add(new CursoSeccion
                {
                  Curso = curso,
                  CursoId = curso.CursoId,
                  Seccion = seccion,
                  SeccionId = seccion.SeccionId

                });
              }

              var result = await this.context.SaveChangesAsync();
              if (result > 0)
                return Unit.Value;

            }
          }
          return Unit.Value;
        }

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar el curso" });


      }
    }

  }
}