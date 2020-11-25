using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Usuarios
{
  public class Consulta
  {
    public class Ejecuta : IRequest<List<DtUsuario>>
    {
    }


    public class Manejador : IRequestHandler<Ejecuta, List<DtUsuario>>
    {
      private readonly UserManager<Usuario> userManager;
      private readonly UdelarOnlineContext context;

      public Manejador(UserManager<Usuario> userManager, UdelarOnlineContext context)
      {
        this.userManager = userManager;
        this.context = context;
      }

      public async Task<List<DtUsuario>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var usuarios = await this.context.Users.Include(u => u.Facultad).Include(u => u.ComunicadoLista).ToListAsync();

        var dtUsuarios = new List<DtUsuario>();
        foreach (var usuario in usuarios)
        {
          DtFacultad dtFacultad = null;
          if (usuario.Facultad != null)
          {
            var facultad = usuario.Facultad;
            dtFacultad = new DtFacultad
            {
              FacultadId = facultad.FacultadId,
              Nombre = facultad.Nombre,
              Descripcion = facultad.Descripcion,
              DominioMail = facultad.DominioMail,
              UrlAcceso = facultad.UrlAcceso
            };
          }
           
          var dtUsuario = new DtUsuario
          {
            Id = usuario.Id,
            Nombres = usuario.Nombres,
            Apellidos = usuario.Apellidos,
            CI = usuario.CI,
            Direccion = usuario.Direccion,
            Email = usuario.Email,
            emailPersonal = usuario.EmailPersonal,
            Facultad = dtFacultad,
            FechaNacimiento = usuario.FechaNacimiento,
            Telefono = usuario.Telefono,
            Tipo = usuario.GetType().ToString().Split('.')[1],
            UserName = usuario.UserName
          };

          dtUsuarios.Add(dtUsuario);

        };

        return dtUsuarios;
      }
    }
  }
}