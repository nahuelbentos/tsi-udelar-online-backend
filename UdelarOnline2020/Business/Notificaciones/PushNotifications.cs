using MediatR;
using Business.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Business.Notificaciones
{
    public class PushNotifications
    {
        public class Ejecuta : IRequest<bool> {}
        public class Manejador : IRequestHandler<Ejecuta, bool>
        {
        private readonly IPushGenerator pushGenerator;

        public Manejador(IPushGenerator pushGenerator)
        {
            this.pushGenerator = pushGenerator;
        }

        public async Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            return await Task.FromResult( this.pushGenerator.SendPushPrueba()) ; 
        }
        }  
    }
}