using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Business.Roles
{
  public class Consulta
  {
    public class Ejecuta : IRequest<List<IdentityRole>>
    {

    }

    public class Manejador : IRequestHandler<Ejecuta, List<IdentityRole>>
    {
      private readonly RoleManager<IdentityRole> roleManager;
      private readonly UdelarOnlineContext context;

      public Manejador(RoleManager<IdentityRole> roleManager, UdelarOnlineContext context)
      {
        this.roleManager = roleManager;
        this.context = context;
      }

      public async Task<List<IdentityRole>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var roles = await this.context.Roles.ToListAsync();

        return roles;

      }
    }

  }
}