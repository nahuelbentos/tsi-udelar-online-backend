using System;

namespace Models
{
  public class UsuarioCurso
  {
    public Guid UsuarioId { get; set; }

    public Usuario Usuario { get; set; }

    public Guid CursoId { get; set; }

    public Curso Curso { get; set; }
  }
}