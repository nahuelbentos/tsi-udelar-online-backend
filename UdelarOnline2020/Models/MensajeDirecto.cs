using System;

namespace Models
{
  public class MensajeDirecto : Mensaje
  {
    public Guid ReceptorId { get; set; }
    public Usuario Receptor { get; set; }
  }
}