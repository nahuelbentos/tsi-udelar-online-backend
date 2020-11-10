using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using FluentValidation;
using MediatR;

namespace Business.Seguridad
{
    public class ForgotPassword
    {
        
        public class Ejecuta : IRequest<DtUsuario>{

            public string Email { get; set; }
            public string PasswordOld { get; set; }
            public string PasswordNew { get; set; }
            public string PasswordConfirm { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(u => u.Email).NotEmpty();
                RuleFor(u => u.PasswordOld).NotEmpty();
                RuleFor(u => u.PasswordNew).NotEmpty();
                RuleFor(u => u.PasswordConfirm).NotEmpty();
            }
        }

    public class Manejador : IRequestHandler<Ejecuta, DtUsuario>
    {
      public Task<DtUsuario> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        throw new System.NotImplementedException();
      }
    }

  }
}