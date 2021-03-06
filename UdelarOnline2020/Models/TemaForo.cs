using System;
using System.Collections.Generic;

namespace Models
{
  public class TemaForo
  {
    public Guid TemaForoId { get; set; }
    public string Asunto { get; set; }
    public DateTime FechaCreado { get; set; }
    public string Mensaje { get; set; }
    public string EmisorId { get; set; }
    public Usuario Emisor { get; set; }
    public byte[] ArchivoData { get; set; } 
    public string ArchivoNombre { get; set; }
    public string ArchivoExtension { get; set; }
    public bool SubscripcionADiscusion { get; set; }

    public Guid ForoId { get; set; }
    public Foro Foro { get; set; }


    public ICollection<MensajeTema> MensajeTemaLista { get; set; }
  }
}