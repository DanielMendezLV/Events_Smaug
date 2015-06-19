using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Eventos.Models
{
    public class EventosContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public EventosContext() : base("name=EventosContext")
        {
        }

        public System.Data.Entity.DbSet<Eventos.Models.Evento> Eventoes { get; set; }

        public System.Data.Entity.DbSet<Eventos.Models.Tipo> Tipoes { get; set; }

        public System.Data.Entity.DbSet<Eventos.Models.Evento_Seccion> Evento_Seccion { get; set; }

        public System.Data.Entity.DbSet<Eventos.Models.Lugar> Lugars { get; set; }

        public System.Data.Entity.DbSet<Eventos.Models.Seccion> Seccions { get; set; }

        public System.Data.Entity.DbSet<Eventos.Models.Asiento> Asientoes { get; set; }

        public System.Data.Entity.DbSet<Eventos.Models.Entrada> Entradas { get; set; }

        public System.Data.Entity.DbSet<Eventos.Models.Factura> Facturas { get; set; }

        public System.Data.Entity.DbSet<Eventos.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<Eventos.Models.Factura_Entrada> Factura_Entrada { get; set; }

        public System.Data.Entity.DbSet<Eventos.Models.Rol> Rols { get; set; }
    
    }
}
