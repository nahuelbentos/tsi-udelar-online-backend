using System;

namespace Models
{
  public class UsuarioEnviaMensaje
  {
    public Guid UsuarioId { get; set; }
    public Usuario UsuarioEnvia { get; set; }

    public Guid MensajeId { get; set; }
    public Mensaje Mensaje { get; set; }

  }
}