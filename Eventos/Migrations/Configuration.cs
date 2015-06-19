namespace Eventos.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Eventos.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Eventos.Models.EventosContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Eventos.Models.EventosContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            ////

            //context.Rols.AddOrUpdate(
            //        p => p.IdRol,
            //        new Rol { IdRol = 1, Nombre = "Admin" },
            //        new Rol { IdRol = 2, Nombre = "Users" }
            //);


            //context.Lugars.AddOrUpdate(
            //        l => l.IdLugar,
            //        new Lugar { IdLugar = 1, Nombre = "Estadio Mateo Flores", Direccion = "10 avenida de la zona 5.", Foto = "estadiomateo.jpg", NoEntradas = 20000 },
            //        new Lugar { IdLugar = 2, Nombre = "Estadio del Ejercito", Direccion = "Calle Mariscal Cruz y 12 avenida, zona 5, Ciudad de Guatemala", Foto = "estadioejercito.jpg", NoEntradas = 12000 }
            //);

            //context.Tipoes.AddOrUpdate(
            //        t => t.IdTipo,
            //        new Tipo { IdTipo = 1, Nombre = "Concierto" },
            //        new Tipo { IdTipo = 2, Nombre = "Tertulia" },
            //        new Tipo { IdTipo = 2, Nombre = "Obra Teatral" }

            //    );


            //context.Eventoes.AddOrUpdate(
            //         l => l.IdEvento,
            //         new Evento { IdEvento = 1, Nombre = "Concierto: Avenged Sevenfold", Fecha = DateTime.Now, EntradasDisponibles = 2000, Foto = "Avg.png", LugarId = 1,TipoId=1},
            //          new Evento { IdEvento = 2, Nombre = "Concierto: Guns n Roses", Fecha = DateTime.Now, EntradasDisponibles = 10000, Foto = "guns.png", LugarId = 2,TipoId=1 }
            //    );

            

            //context.Evento_Tipo.AddOrUpdate(
            //       t => t.TipoId,
            //        new Evento_Tipo { EventoId=1, TipoId=1 },
            //        new Evento_Tipo { EventoId = 2, TipoId = 1 }
            //        );




           
        }
    }
}
