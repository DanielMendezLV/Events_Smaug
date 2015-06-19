using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventos.Models
{
    public class Lugar
    {
        [Key]
        public int IdLugar { get; set; }
        [Required]
        public String Nombre { get; set; }
        [Required]
        public String Direccion { get; set; }
        [Required]
        public int NoEntradas { get; set; }
        [Required]
        public String Foto { get; set; }

    }
}