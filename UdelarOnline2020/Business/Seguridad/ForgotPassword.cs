using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;
using Business.ManejadorError;
using Business.Interfaces;
using System.Collections.Generic;
using System;

namespace Business.Seguridad
{
    public class ForgotPassword
    {
        
        public class Ejecuta : IRequest<DtUsuario>{

            public string Token { get; set; } 
            public string Email { get; set; } 
            public string PasswordNew { get; set; } 
        }
 
    public class Manejador : IRequestHandler<Ejecuta, DtUsuario>
    {
      private readonly UserManager<Usuario> userManager;
      private readonly UdelarOnlineContext context;
      private readonly SignInManager<Usuario> signInManager;
      private readonly IJwtGenerador jwtGenerador;

      public Manejador(UserManager<Usuario> userManager, UdelarOnlineContext context,SignInManager<Usuario> signInManager, IJwtGenerador jwtGenerador)
      {
        this.userManager = userManager;
        this.context = context;
        this.signInManager = signInManager;
        this.jwtGenerador = jwtGenerador;
      }

      public async Task<DtUsuario> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        // esto no esta muy bien...
        // var usuario = await this.context.Users.Where( u => u.Email.Contains(request.Email) || u.EmailPersonal.Contains(request.Email) ).FirstOrDefaultAsync();
        var usuario = await this.userManager.FindByEmailAsync(request.Email);

        if (usuario == null) 
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con el email ingresado."});
        
        var resetPassResult = await this.userManager.ResetPasswordAsync(usuario, request.Token, request.PasswordNew);
        
        Console.WriteLine("token:: " +  request.Token);
        Console.WriteLine("usuario:: " +  usuario.Id);
        Console.WriteLine("usuario:: " +  usuario.EmailPersonal);
        Console.WriteLine("usuario:: " +  usuario.Nombres);
        // var newPassword = this.userManager.PasswordHasher.HashPassword(usuario, request.PasswordNew);
        // usuario.PasswordHash = newPassword;
        // var res = await this.userManager.UpdateAsync(usuario);
        if(resetPassResult.Succeeded){


          var resultado = await signInManager.CheckPasswordSignInAsync(usuario, request.PasswordNew, false);

          if (resultado.Succeeded)
          { 
            var listaRoles = await this.userManager.GetRolesAsync(usuario);
            var roles = new List<string>(listaRoles);
            var facultad = await this.context.Facultad.FindAsync(usuario.Facultad.FacultadId);
            // La idea es que solo haya un único rol por usuario.
            var rol = "";
            if (roles.Count > 0)
              rol = roles[0];
            var dtFacultad = new DtFacultad
            {

              Id = facultad.FacultadId,
              FacultadId = facultad.FacultadId,
              Descripcion = facultad.Descripcion,
              Nombre = facultad.Nombre,
              UrlAcceso = facultad.UrlAcceso,
              DominioMail = facultad.DominioMail,
              ColorCodigo = facultad.ColorCodigo,
            };

            return new DtUsuario
            {
              Id = usuario.Id,
              Nombres = usuario.Nombres,
              Apellidos = usuario.Apellidos,
              emailPersonal = usuario.EmailPersonal,
              CI = usuario.CI,
              Token = this.jwtGenerador.CrearToken(usuario, roles),
              Email = usuario.Email,
              UserName = usuario.UserName,
              Tipo = usuario.GetType().ToString().Split('.')[1],
              Rol = rol,
              Facultad = dtFacultad
            };

          }
        }


        var mensajeErrores = resetPassResult.Errors.Select(e => e.Description);
        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrió un error, no se pudo reestablecer la contraseña", mensajeErrores });
        // throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Ocurrio un error inesperado. " }); 
      }
    }

  }
}