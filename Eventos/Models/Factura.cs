using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eventos.Models
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }

        public int UsuarioId { get; set; }
       
        public int Total { get; set; }
        public Usuario usuario { get; set; }
       
    }
}