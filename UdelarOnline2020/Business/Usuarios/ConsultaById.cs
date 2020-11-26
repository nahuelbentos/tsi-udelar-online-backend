using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using Business.Datatypes;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Usuarios
{
  public class ConsultaById
  {

    public class Ejecuta : IRequest<DtUsuario>
    {
      public string Id { get; set; }
    }


    public class Manejador : IRequestHandler<Ejecuta, DtUsuario>
    {
      private readonly UserManager<Usuario> userManager;
      private readonly UdelarOnlineContext context;

      public Manejador(UserManager<Usuario> userManager, UdelarOnlineContext context)
      {
        this.userManager = userManager;
        this.context = context;
      }

      public async Task<DtUsuario> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        // var usuario = await this.userManager.FindByIdAsync(request.Id);
        var usuario = await this.context.Users.Include(u => u.Facultad).Include(u => u.ComunicadoLista).FirstOrDefaultAsync(u => u.Id == request.Id);
        Console.WriteLine(usuario.Facultad.FacultadId);


        if (usuario == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con ese Id." });
        
        var facultad = usuario.Facultad;
        var dtFacultad = new DtFacultad
        {
          Nombre = facultad.Nombre,
          Descripcion = facultad.Descripcion,
          DominioMail = facultad.DominioMail,
          UrlAcceso = facultad.UrlAcceso,
          FacultadId = facultad.FacultadId
        };

        return new DtUsuario
        {
          Id = usuario.Id,
          Nombres = usuario.Nombres,
          Apellidos = usuario.Apellidos,
          emailPersonal = usuario.EmailPersonal,
          CI = usuario.CI, 
          Email = usuario.Email,
          UserName = usuario.UserName,
          Telefono = usuario.Telefono,
          Direccion = usuario.Direccion,
          FechaNacimiento = usuario.FechaNacimiento,
          Tipo = usuario.GetType().ToString().Split('.')[1],
          Facultad = dtFacultad
        }; 
      }

    }
  }
}