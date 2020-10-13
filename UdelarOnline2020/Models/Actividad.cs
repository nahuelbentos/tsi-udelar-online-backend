using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Actividad
    {
        [Key]
        public int Id { get; set; }
        public DateTime FechaRealizada { get; set; }
        public DateTime FechaFinalizada { get; set; }
    }
}
