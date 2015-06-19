using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventos.Models
{
    public class Evento
    {
        [Key]
        public int IdEvento { get; set; }
        [Required]
        public String Nombre { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int EntradasDisponibles { get; set; }
        [Required]
        public String Foto { get; set; }
        [Required]
        public int LugarId { get; set; }
        [Required]
        public int TipoId { get; set; }

        public Lugar Lugar { get; set; }
        public Tipo Tipo { get; set; }

        [JsonIgnore]
        public virtual ICollection<Evento_Seccion> Evento_Seccion { get; set; }
        


    }
}