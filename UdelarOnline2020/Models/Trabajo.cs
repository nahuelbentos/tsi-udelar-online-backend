using System;
using System.IO;

namespace Models
{
  public class Trabajo : Actividad
  {
    public byte[] Archivo { get; set; }

    public bool EsIndividual { get; set; }

    public int Calificacion { get; set; }

    public string Nota { get; set; }
  }
}