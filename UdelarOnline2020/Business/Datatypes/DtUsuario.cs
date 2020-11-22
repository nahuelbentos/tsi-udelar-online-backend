using System;
using System.Collections.Generic;
using Models;

namespace Business.Datatypes
{
  public class DtUsuario
  {

    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string CI { get; set; }
    public DateTime FechaNacimiento { get; set; }

    public string Direccion { get; set; }
    public string Telefono { get; set; }


    public string emailPersonal { get; set; }

    public string Token { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Tipo { get; set; }
    public DtFacultad Facultad { get; set; }
    public string Rol { get; set; }
  }
}