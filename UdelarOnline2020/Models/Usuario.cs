using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Models
{
  public class Usuario : IdentityUser
  {


    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string CI { get; set; }
    public DateTime FechaNacimiento { get; set; }

    public string Direccion { get; set; }
    public string Telefono { get; set; }

    public string EmailPersonal { get; set; }
    public Facultad Facultad { get; set; }

    public  ICollection<Comunicado> ComunicadoLista { get; set; }
  }
}
