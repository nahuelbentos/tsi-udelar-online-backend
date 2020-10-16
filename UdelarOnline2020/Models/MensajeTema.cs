using System;

namespace Models
{
  public class MensajeTema : Mensaje
  {
    public bool MensajeBloqueado { get; set; }

    public Guid TemaForoId { get; set; }

    public TemaForo TemaForo { get; set; }
  }
}