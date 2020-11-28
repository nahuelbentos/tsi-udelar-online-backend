using System;
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
  public class ConsultaDocenteByFacultad
  {


    public class Ejecuta : IRequest<List<DtUsuario>>
    {
      public Guid FacultadId { get; set; }

    }

    public class Manejador : IRequestHandler<Ejecuta, List<DtUsuario>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<DtUsuario>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var facultadDb = await this.context.Facultad.FindAsync(request.FacultadId);

        if (facultadDb == null)
          throw new ManejadorExcepcion(HttpStatusCode.Forbidden, new { mensaje = "No existe un facultad con el FacultadId ingresado" });

        var docentes = await this.context.Usuario
                                          .Include(u => u.Facultad)
                                          .Where(u => u.Facultad.FacultadId == request.FacultadId && (u is Docente))
                                          .ToListAsync();

        List<DtUsuario> dtUsuarios = new List<DtUsuario>();

        foreach (var usuario in docentes)
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