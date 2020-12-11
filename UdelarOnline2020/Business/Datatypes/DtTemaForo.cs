using System;
using System.Collections.Generic;
using Models;

namespace Business.Datatypes
{
    public class DtTemaForo
    {

    public Guid TemaForoId { get; set; }
    public string Asunto { get; set; }
    public string Mensaje { get; set; }
    public string EmisorId { get; set; }
    public string Emisor { get; set; }
    public Usuario EmisorData { get; set; }
    public byte[] ArchivoData { get; set; }
    public string ArchivoNombre { get; set; }
    public string ArchivoExtension { get; set; }
    public bool SubscripcionADiscusion { get; set; }
    public DateTime FechaCreado { get; set; }
    public Guid ForoId { get; set; }
    public Foro Foro { get; set; }
    public List<DtMensajeTema> ListaMensajeTema { get; set; }
    
    
    }
}