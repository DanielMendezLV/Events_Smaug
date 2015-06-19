using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventos.Models
{
    public class Entrada
    {
        [Key]
        public int IdEntrada { get; set; }

        [Required]
        public String Fecha { get; set; }
       

        public int AsientoId { get; set; }
        public int SeccionId { get; set; }
        public int UsuarioId { get; set; }
        public int EventoId { get; set; }


        public Asiento Asiento { get; set; }
        public Seccion Seccion { get; set; }
        public Usuario Usuario { get; set; }
        public Evento Evento { get; set; }
        
    }
}