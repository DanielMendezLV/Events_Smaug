using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eventos.Models
{
    public class Seccion
    {
        [Key]
        public int IdSeccion { get; set; }
        [Required]
        public String Nombre { get; set; }
        [Required]
        public int NoAsientos { get; set; }
        [Required]
        public int Precio { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public String Numeracion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Evento_Seccion> Evento_Seccion { get; set; }
    }
}