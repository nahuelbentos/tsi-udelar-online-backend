using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using Business.Datatypes;
using Business.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Notificaciones
{
  public class SendMail
  {
    public class Ejecuta : IRequest<bool> { }
    public class Manejador : IRequestHandler<Ejecuta, bool>
    {
      private readonly IMailGenerator mailGenerator;

      public Manejador(IMailGenerator mailGenerator)
      {
        this.mailGenerator = mailGenerator;
      }

      public async Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        return await Task.FromResult( this.mailGenerator.SendMailPrueba() ) ; 
      }
    }  
  }
}