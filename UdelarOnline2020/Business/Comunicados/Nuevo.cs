using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Business.Datatypes;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Comunicados {
  public class Nuevo {
    public class Ejecuta : IRequest {
      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public string Url { get; set; }
      public string usuarioEmail { get; set; }
      //public DtUsuario dtUsuario { get; set; }

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta> {
      public EjecutaValidator () {
        RuleFor (t => t.Nombre).NotEmpty ().WithMessage ("El nombre es requerido.");
        RuleFor (t => t.Descripcion).NotEmpty ();
        RuleFor (t => t.Url).NotEmpty ();
        RuleFor (t => t.usuarioEmail).NotEmpty ();
      }
    }

    public class Manejador : IRequestHandler<Ejecuta> {
      private readonly UdelarOnlineContext context;

      public Manejador (UdelarOnlineContext context) {
        this.context = context;
      }

      public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
      var usuario = await this.context.Usuario.Where(e => e.Email == request.usuarioEmail).FirstOrDefaultAsync();
      
      if (usuario == null)
      {
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el usuario ingresado" });
      }
      var comunicado = new Comunicado {
        ComunicadoId = Guid.NewGuid (),
        Nombre = request.Nombre,
        Descripcion = request.Descripcion,
        Url = request.Url,
        usuario = usuario
      };

   
        //usuario.ComunicadoLista.Add(comunicado);
        //await this.context.Usuario.AddAsync(usuario);
        this.context.Comunicado.Add(comunicado);

        var res = await this.context.SaveChangesAsync ();
        if (res > 0) {
          return Unit.Value;
        }

        throw new Exception ("No se pudo dar de alta el comunicado");

      }
    }
  }
}