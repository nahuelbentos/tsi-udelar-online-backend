using System;
using Models;

namespace Business.Datatypes
{
    public class DtMensajeTema
    {

    public Guid MensajeId { get; set; }

    public string Contenido { get; set; }

    public DateTime FechaDeEnviado { get; set; }

    public Usuario Emisor { get; set; }
    public Guid EmisorId { get; set; }

    public bool MensajeBloqueado { get; set; }

    public Guid TemaForoId { get; set; }

    public TemaForo TemaForo { get; set; }
    }
}