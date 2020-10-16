using System;

namespace Models
{
    public class TemaForo : Foro
    {
        public String Asunto { get; set; }

        public String Mensaje { get; set; }

        //public File ArchivoAdjunto { get; set; }

        public Boolean SubscripcionADiscusion { get; set; }
    }
}