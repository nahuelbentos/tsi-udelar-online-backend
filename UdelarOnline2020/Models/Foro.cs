using System;
using System.Collections.Generic;

namespace Models
{
  public class Foro
  {
    public Guid ForoId { get; set; }

    public String Titulo { get; set; }

    public String Descripcion { get; set; }


    public ICollection<TemaForo> TemaForoLista { get; set; }
  }
}