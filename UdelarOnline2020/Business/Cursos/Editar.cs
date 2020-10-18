using System.Net;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;
using Microsoft.Extensions.Logging;
using Aplicacion.ManejadorError;

namespace Business.Cursos
{
  public class Editar
  {
    public class Ejecuta : IRequest
    {
      public Guid CursoId { get; set; }
      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public ModalidadEnum? ModalidadCurso { get; set; }
      public bool? RequiereMatriculacion { get; set; }
      public string SalaVirtual { get; set; }
      public Guid TemplateCursoId { get; set; }
    }


    public class EjecutaValidacion : AbstractValidator<Ejecuta>
    {
      public EjecutaValidacion()
      {
        RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre es requerido");
        RuleFor(c => c.ModalidadCurso).NotEmpty().WithMessage("Es requerido enviar ModalidadCurso, incluso si es la misma que la anterior.");
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
        var curso = await this.context.Curso.FindAsync(request.CursoId);

        if (curso == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El curso no existe" });

        }

        if (request.TemplateCursoId != Guid.Empty)
        {

          var templateCurso = await this.context.TemplateCurso.Where(tc => tc.TemplateCursoId == request.TemplateCursoId).FirstOrDefaultAsync();


          if (templateCurso == null)
          {
            throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El template enviado,  no existe." });
          }

          curso.TemplateCursoId = request.TemplateCursoId;
          curso.TemplateCurso = templateCurso;

        }

        curso.Nombre = request.Nombre ?? curso.Nombre;
        curso.Descripcion = request.Descripcion ?? curso.Descripcion;
        curso.Modalidad = request.ModalidadCurso ?? curso.Modalidad;
        curso.RequiereMatriculacion = request.RequiereMatriculacion ?? curso.RequiereMatriculacion;
        curso.SalaVirtual = request.SalaVirtual ?? curso.SalaVirtual;


        var res = await this.context.SaveChangesAsync();


        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el curso" });

      }
    }




  }
}