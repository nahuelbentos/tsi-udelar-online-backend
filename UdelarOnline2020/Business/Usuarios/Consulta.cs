using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    public class Ejecuta : IRequest<List<Usuario>>
    {
    }


    public class Manejador : IRequestHandler<Ejecuta, List<Usuario>>
    {
      private readonly UserManager<Usuario> userManager;
      private readonly UdelarOnlineContext context;

      public Manejador(UserManager<Usuario> userManager, UdelarOnlineContext context)
      {
        this.userManager = userManager;
        this.context = context;
      }

      public async Task<List<Usuario>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var usuarios = await this.context.Users.Include(u => u.Facultad).Include(u => u.ComunicadoLista).ToListAsync();

        return usuarios;
      }
    }
  }
}