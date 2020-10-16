using System;

namespace Models
{
  public class UsuarioRecibeMensaje
  {

    public Guid UsuarioId { get; set; }
    public Usuario UsuarioRecibe { get; set; }

    public Guid MensajeId { get; set; }
    public Mensaje Mensaje { get; set; }
  }
}