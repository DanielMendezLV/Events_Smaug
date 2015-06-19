using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventos.Models
{
    public class Asiento
    {
        [Key]
        public int IdAsiento { get; set; }
        [Required]
        public String Numero { get; set; }

        public int SeccionId { get; set; }

        public Boolean Estado { get; set; }

        public Seccion Seccion { get; set; }

    }
}