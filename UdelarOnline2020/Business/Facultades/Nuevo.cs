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

namespace Business.Facultades
{
  public class Nuevo
  {
    public class Ejecuta : IRequest
    {

      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public string UrlAcceso { get; set; }
      public string DominioMail { get; set; } // Ejemplo: @fing.edu.uy
      public string LogoNombre { get; set; }
      public string LogoExtension { get; set; }
      public string LogoData { get; set; }
      public string ColorCodigo { get; set; }

    }

    public class EjecutaValidador : AbstractValidator<Ejecuta>
    {
      public EjecutaValidador()
      {
        RuleFor(f => f.Nombre).NotEmpty();
        RuleFor(f => f.Descripcion).NotEmpty();
        RuleFor(f => f.UrlAcceso).NotEmpty();
        RuleFor(f => f.DominioMail).NotEmpty();
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


        var otraFacultadConMisoDominio = await this.context.Facultad.Where(f => f.DominioMail == request.DominioMail).FirstOrDefaultAsync();

        if (otraFacultadConMisoDominio != null)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Existe otra facultad con el mismo Dominio de Mail, ingrese uno distinto." });

        var facultad = new Facultad
        {
          FacultadId = Guid.NewGuid(),
          Nombre = request.Nombre,
          Descripcion = request.Descripcion,
          UrlAcceso = request.UrlAcceso,
          DominioMail = request.DominioMail,

          LogoData = request.LogoData,
          LogoExtension = request.LogoExtension,
          LogoNombre = request.LogoNombre,
          ColorCodigo = request.ColorCodigo,
        };

        await this.context.Facultad.AddAsync(facultad);

        var res = await this.context.SaveChangesAsync();


        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo insertar la facultad" });
      }
    }
  }
}