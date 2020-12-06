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
    public class ConsultaAlumnoByCurso
    {

    public class Ejecuta : IRequest<List<DtUsuario>>
    {
      public Guid CursoId { get; set; }

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
        var curso = await this.context.Curso.FindAsync(request.CursoId);

        if (curso == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un curso con el CursoId ingresado" });

        var alumnos = await this.context.AlumnoCurso
                                          .Include(uc => uc.Curso)
                                          .Include(uc => uc.Alumno).ThenInclude(u => u.Facultad)
                                          .Where(uc => uc.CursoId == request.CursoId)
                                          .Select(uc => uc.Alumno)
                                          .ToListAsync();

        List<DtUsuario> dtUsuarios = new List<DtUsuario>();

        foreach (var usuario in alumnos)
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