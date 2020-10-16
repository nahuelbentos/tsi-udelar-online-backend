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

using Persistence;
using Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebAPI.Middleware;
using Microsoft.EntityFrameworkCore;

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
      services.AddControllers();


      services.AddDbContext<UdelarOnlineContext>(opt =>
      {
        opt.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
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

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseMiddleware<ManejadorErrorMiddleware>();

      app.UseCors(builder =>
      {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
      });

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
