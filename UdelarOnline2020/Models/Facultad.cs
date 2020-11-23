using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
  public class Facultad
  {

    public Guid FacultadId { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string UrlAcceso { get; set; }
    public string DominioMail { get; set; } // Ejemplo: @fing.edu.uy
    public string LogoNombre { get; set; } 
    public string LogoExtension { get; set; } 
    public string LogoData { get; set; } 
    public string ColorCodigo { get; set; } 

    public ICollection<Usuario> UsuarioLista { get; set; }

    public ICollection<Encuesta> ListaEncuesta { get; set; }
  }
}
