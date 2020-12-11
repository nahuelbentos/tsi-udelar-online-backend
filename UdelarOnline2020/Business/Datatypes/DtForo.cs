using System;
using System.Collections.Generic;

namespace Business.Datatypes
{
    public class DtForo
    {

    public Guid ForoId { get; set; }

    public string Titulo { get; set; }

    public string Descripcion { get; set; }


    public List<DtTemaForo> TemaForoLista { get; set; }
    }
}