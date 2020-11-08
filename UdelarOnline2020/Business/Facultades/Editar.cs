using System.Net;
using System;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Business.Facultades
{
  public class Editar
  {
    public class Ejecuta : IRequest
    {

      public Guid FacultadId { get; set; }
      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public string UrlAcceso { get; set; }
      public string DominioMail { get; set; } // Ejemplo: @fing.edu.uy
      public string LogoNombre { get; set; }
      public string LogoExtension { get; set; }
      public string LogoData { get; set; }
      public string ColorCodigo { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var facultad = await this.context.Facultad.FindAsync(request.FacultadId);
        if (facultad == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe una facultad con el Id ingresado." });

        var otraFacultadConMisoDominio = await this.context.Facultad.Where(f => f.DominioMail == request.DominioMail && !f.FacultadId.Equals(request.FacultadId)).FirstOrDefaultAsync();

        if (otraFacultadConMisoDominio != null)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Existe otra facultad con el mismo Dominio de Mail, ingrese uno distinto." });

        facultad.Nombre = request.Nombre ?? facultad.Nombre;
        facultad.Descripcion = request.Descripcion ?? facultad.Descripcion;
        facultad.UrlAcceso = request.UrlAcceso ?? facultad.UrlAcceso;
        facultad.DominioMail = request.DominioMail ?? facultad.DominioMail;

        facultad.LogoNombre = request.LogoNombre ?? facultad.LogoNombre;
        facultad.LogoData = request.LogoData ?? facultad.LogoData;
        facultad.LogoExtension = request.LogoExtension ?? facultad.LogoExtension;
        facultad.ColorCodigo = request.ColorCodigo ?? facultad.ColorCodigo;


        var res = await this.context.SaveChangesAsync();


        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar la facultad" });
      }
    }

  }

}