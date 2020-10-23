using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Business;
using Persistence;
using Models;
using MediatR;
using Business.Cursos;
using Seguridad.Token;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace WebAPI
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddDbContext<UdelarOnlineContext>(opt =>
      {
        opt.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
      });


      services.AddMediatR(typeof(Consulta.Manejador).Assembly);

      services.AddControllers(opt =>
      {
        // Declaro politica para requerir Autenticación y la agrego como filtro.
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        opt.Filters.Add(new AuthorizeFilter(policy));
      })
       .AddFluentValidation(config =>
       {
         config.RegisterValidatorsFromAssemblyContaining<Business.Cursos.Nuevo>();
       });



      // Configuración de IdentityCore
      var builder = services.AddIdentityCore<Usuario>();
      var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);

      //Configuración de roles y Rol con Tokens -> Ademas instanciamos el servicio de RoleManager
      identityBuilder.AddRoles<IdentityRole>();
      identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Usuario, IdentityRole>>();

      identityBuilder.AddEntityFrameworkStores<UdelarOnlineContext>();
      identityBuilder.AddSignInManager<SignInManager<Usuario>>();

      services.TryAddSingleton<ISystemClock, SystemClock>();


      // Configuración para los JWT Tokens
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Udelar Online TSI"));
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
      {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = key,
          ValidateAudience = false, // Alguien con una IP cualquiera pueda generar un Token
          ValidateIssuer = false, // Es para el envio del token (?)
        };
      });

      services.AddScoped<IJwtGenerador, JwtGenerador>();

      // Middleware

      services.AddMediatR(typeof(Editar.Manejador).Assembly);

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }


      app.UseCors(builder =>
      {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
      });

      // app.UseHttpsRedirection();

      app.UseAuthentication();

      app.UseRouting();

      app.UseMiddleware<ManejadorErrorMiddleware>();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
