using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eventos.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        [Required]
        public String Nombre { get; set; }
    }
}