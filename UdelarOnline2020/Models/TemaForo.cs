using System;

namespace Models
{
  public class TemaForo
  {
    public Guid TemaForoId { get; set; }
    public string Asunto { get; set; }

    public string Mensaje { get; set; }

    public Guid EmisorId { get; set; }
    public Usuario Emisor { get; set; }

    //public File ArchivoAdjunto { get; set; }
    public bool SubscripcionADiscusion { get; set; }
  }
}