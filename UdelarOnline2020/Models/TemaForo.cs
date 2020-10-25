using System;

namespace Models
{
  public class TemaForo
  {
    public Guid TemaForoId { get; set; }
    public string Asunto { get; set; }
    public string Mensaje { get; set; }
    public string EmisorId { get; set; }
    public Usuario Emisor { get; set; }
    public string ArchivoAdjunto { get; set; } //File
    public bool SubscripcionADiscusion { get; set; }
  }
}