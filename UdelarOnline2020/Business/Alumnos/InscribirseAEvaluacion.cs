using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Alumnos
{
  public class InscribirseAEvaluacion
  {
    public class Ejecuta : IRequest
    {
      public string AlumnoId { get; set; }

      public Guid PruebaOnlineId { get; set; }

    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;
      private readonly IBedeliasGenerator bedelias;
      private readonly IPushGenerator pushGenerator;
      private readonly IMailGenerator mailGenerator;

      public Manejador(UdelarOnlineContext context, IBedeliasGenerator bedelias, IPushGenerator pushGenerator, IMailGenerator mailGenerator)
      {
        this.context = context;
        this.bedelias = bedelias;
        this.pushGenerator = pushGenerator;
        this.mailGenerator = mailGenerator;
      }
      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var alumno = await this.context.Alumno.FindAsync(request.AlumnoId);
        if (alumno == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El alumno no existe." });

        var pruebaOnline = await this.context.PruebaOnline.FindAsync(request.PruebaOnlineId);
        if (pruebaOnline == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La Prueba Online no existe." });
        
        var curso = await this.context.CursoSeccionActividad
                                              .Include( a => a.Curso)
                                              .Where( x => x.ActividadId == pruebaOnline.ActividadId)
                                              .Select( cs => cs.Curso)
                                              .FirstOrDefaultAsync();
        if (curso == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La Prueba Online no esta vinculada a ningún curso." });

        var estaInscripto = await this.context.AlumnoPruebaOnline
                                                .Include(apo => apo.Alumno)
                                                .Include(apo => apo.PruebaOnline)
                                                .Where( apo => apo.AlumnoId == Guid.Parse(alumno.Id)  && apo.PruebaOnlineId == pruebaOnline.ActividadId)
                                                .FirstOrDefaultAsync();
        if(estaInscripto != null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El alumno ya está inscripto en la evaluación." });
        
        var inscripto = true;
        // var inscripto = await this.bedelias.AprobarInscripcionEvaluacion(alumno.CI, curso.CursoId);

        // if (!inscripto)
        //   throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Bedelías rechazo la inscripcion, comuniquese con un administrador." });
        
        if (alumno.EmailPersonal != "")
          this.mailGenerator.mailInscripcionEvaluacion(alumno.EmailPersonal, $"Inscripción a la evaluación: {pruebaOnline.Nombre} {pruebaOnline.Descripcion}", pruebaOnline);
       
        if (alumno.TokenPush != "") {
          List<string> token = new List<string>();
          token.Add (alumno.TokenPush);
          pushGenerator.SendPushNotifications ("Inscripción Correcta!", "Te has inscripto correctamente a la evaluación " + pruebaOnline.Nombre, token);
        }
         

        var alumnoPruebaOnline = new AlumnoPruebaOnline
        {
          Alumno = alumno,
          AlumnoId = Guid.Parse(alumno.Id),
          Inscripto = inscripto,
          PruebaOnline = pruebaOnline,
          PruebaOnlineId = pruebaOnline.ActividadId
        };

        this.context.AlumnoPruebaOnline.Add(alumnoPruebaOnline);

        if (await this.context.SaveChangesAsync() > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrió un error al inscribir al alumno en la evaluación." });

      }
    }
  }
}