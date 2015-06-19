using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventos.Models
{
    public class Factura_Entrada
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int EntradaId{ get; set; }
        public decimal Precio{ get; set; }
        public Factura Factura { get; set; }
        public Entrada Entrada { get; set; }
    }
}