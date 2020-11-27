using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Usuarios
{
    public class ConsultaByRol
    {
        public class Ejecuta : IRequest<List<DtUsuario>>
        {
            public string Rol { get; set; }            
            
        }

    public class Manejador : IRequestHandler<Ejecuta, List<DtUsuario>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador( UdelarOnlineContext context)
      {
        this.context = context;
      }
      public async Task<List<DtUsuario>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        
        List<Usuario> usuarios = null;
        switch (request.Rol)
        {
          case "Administrador":

            usuarios = await this.context.Usuario.Include(u => u.Facultad).Include(u => u.ComunicadoLista).ToListAsync<Usuario>();
            break;
          case "AdministradorFacultad":
            usuarios = await this.context.Usuario
                                          .Include(u => u.Facultad)
                                          .Include(u => u.ComunicadoLista)
                                          .Where(u => !(u is Administrador))
                                          .ToListAsync<Usuario>();
            
            break;
          case "Alumno":
            usuarios = await this.context.Usuario
                                          .Include(u => u.Facultad)
                                          .Include(u => u.ComunicadoLista)
                                          .Where(u => (u is Alumno)) 
                                          .ToListAsync<Usuario>();

            break;
          case "Docente":
            usuarios = await this.context.Usuario
                                          .Include(u => u.Facultad)
                                          .Include(u => u.ComunicadoLista)
                                          .Where(u => !((u is Administrador) && (u is AdministradorFacultad)))
                                          .ToListAsync<Usuario>(); 
            
            break;
          default:
            throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El tipo de usuario debe ser Administrador, AdministradorFacultad, Alumno o Docente" });

        }

        List<DtUsuario> dtUsuarios = new List<DtUsuario>();

        foreach (var usuario in usuarios)
        {

          var facultad = usuario.Facultad;
          var dtFacultad = new DtFacultad
          {
            Nombre = facultad.Nombre,
            Descripcion = facultad.Descripcion,
            DominioMail = facultad.DominioMail,
            UrlAcceso = facultad.UrlAcceso,
            FacultadId = facultad.FacultadId,
            ColorCodigo = facultad.ColorCodigo, 
          };

          dtUsuarios.Add(new DtUsuario
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
          });

        }
        return dtUsuarios;
      }
    }
  }
}