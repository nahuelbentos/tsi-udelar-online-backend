using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Business.Datatypes;
using Business.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;
using Perifericos;

namespace Business.Notificaciones
{
  public class SendMail
  {
    public class Ejecuta : IRequest<bool> { }
    public class Manejador : IRequestHandler<Ejecuta, bool>
    {
      public Manejador()
      {
      }

      public async Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        return MailsTemplates.MailPrueba();   
      }
    }  
  }
}