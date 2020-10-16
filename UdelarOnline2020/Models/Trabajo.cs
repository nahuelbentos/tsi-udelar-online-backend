using System;
using System.IO;

namespace Models
{
    public class Trabajo : Actividad
    {
        // public File Archivo { get; set; }

        public Boolean EsIndividual { get; set; }

        public int Calificacion { get; set; }

        public String Nota { get; set; }
    }
}