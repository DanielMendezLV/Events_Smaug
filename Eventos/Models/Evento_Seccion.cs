using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eventos.Models
{
    public class Evento_Seccion
    {
        [Key, Column(Order = 0)]
        public int EventoId { get; set; }
        [Key, Column(Order = 1)]
        public int SeccionId { get; set; }

        [JsonIgnore]
        public virtual Seccion Seccion { get; set; }
        [JsonIgnore]
        public virtual Evento Evento { get; set; }
    }
}