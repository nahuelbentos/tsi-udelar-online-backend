using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Business.ManejadorError;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Business.Seguridad
{
    public class MailRenovarPassword
    {
        public class Ejecuta : IRequest
        {
            public string Email { get; set; }            
            
        }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;
      private readonly IMailGenerator mailGenerator;
      private readonly UserManager<Usuario> userManager;
      private readonly IJwtGenerador jwtGenerador;

      public Manejador(UdelarOnlineContext context, IMailGenerator mailGenerator, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador)
      {
        this.context = context;
        this.mailGenerator = mailGenerator;
        this.userManager = userManager;
        this.jwtGenerador = jwtGenerador;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        // var usuario = await this.context.Usuario.Where( u => u.Email.Contains(request.Email) || u.EmailPersonal.Contains(request.Email) ).FirstOrDefaultAsync();
        var usuario = await this.userManager.FindByEmailAsync(request.Email);
        if(usuario == null)
            throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe ningún usuario en nuestro sistema con el email ingresado. "});

        // var token = await this.userManager.GeneratePasswordResetTokenAsync(usuario);

        var jwtToken = this.jwtGenerador.TokenResetPassword(usuario, "no hay token");

        // var ok = this.mailGenerator.SendMail(usuario.EmailPersonal, "Renovar Password",);
        var ok = this.mailGenerator.ResetPassword( usuario.Email, "Renovar Password", jwtToken, usuario  );

        if(ok)
            return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrió un error inesperado. " });

      }
    }
  }
}