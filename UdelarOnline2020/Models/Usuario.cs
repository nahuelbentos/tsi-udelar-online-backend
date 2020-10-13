using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Models
{
  public class Usuario : IdentityUser
  {
    [Key]
    public Guid Id { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string CI { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Email { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }

    [Key]
    public string UserName { get; set; }
    public string Password { get; set; }

  }
}
