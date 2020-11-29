using System.Collections.Generic;
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
    public class ConsultaByTipo
    {
        public class Ejecuta : IRequest<List<DtUsuario>>
        {
            public string Tipo { get; set; }            
            
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
        switch (request.Tipo)
        {
          case "Administrador":
            usuarios = await  this.context.Administrador.Include(u => u.Facultad).Include(u => u.ComunicadoLista).ToListAsync<Usuario>();
            break;
          case "AdministradorFacultad":
            usuarios = await this.context.AdministradorFacultad.Include(u => u.Facultad).Include(u => u.ComunicadoLista).ToListAsync<Usuario>();
            
            break;
          case "Alumno":
            usuarios = await this.context.Alumno.Include(u => u.Facultad).Include(u => u.ComunicadoLista).ToListAsync<Usuario>();
            
            break;
          case "Docente":
            usuarios = await this.context.Docente.Include(u => u.Facultad).Include(u => u.ComunicadoLista).ToListAsync<Usuario>();
            
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
            ColorCodigo = facultad.ColorCodigo

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