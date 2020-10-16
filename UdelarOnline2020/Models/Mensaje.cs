using System;

namespace Models
{
  public class Mensaje
  {
    public Guid MensajeId { get; set; }

    public string Contenido { get; set; }

    public DateTime FechaDeEnviado { get; set; }

    public Usuario Emisor { get; set; }
    public Guid EmisorId { get; set; }
  }
}