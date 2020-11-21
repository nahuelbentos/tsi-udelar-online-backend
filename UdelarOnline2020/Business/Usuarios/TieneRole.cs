using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Business.Usuarios
{
    public class TieneRole
    {
    public class Ejecuta: IRequest<Boolean>
    {
        public string Email { get; set; }
        public string RoleName { get; set; }
        
    }

    public class Manejador : IRequestHandler<Ejecuta, Boolean>
    {
      private readonly UserManager<Usuario> userManager;
      private readonly RoleManager<IdentityRole> roleManager;

      public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
      {
        this.userManager = userManager;
        this.roleManager = roleManager;
      }

      public async Task<Boolean> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var role = await this.roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el role ingresado" });

        var user = await this.userManager.FindByEmailAsync(request.Email);
        if (user == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el usuario ingresado" });

        var isInRole = await this.userManager.IsInRoleAsync(user, role.Name);
        return isInRole;
      }
    }

  } 
}