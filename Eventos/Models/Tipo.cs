using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eventos.Models
{
    public class Tipo
    {
        [Key]
        public int IdTipo { get; set; }
        [Required]
        public String Nombre { get; set; }
    }
}