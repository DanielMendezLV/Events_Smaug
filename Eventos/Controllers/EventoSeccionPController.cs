using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Eventos.Models;


namespace Eventos.Controllers
{
   

    public class EventoSeccionPController : ApiController
    {
        private EventosContext db = new EventosContext();

        public IQueryable<Evento_Seccion>  GetEventoSecciones(int id)
        {
            var list = from c in db.Evento_Seccion where c.EventoId==id select c;
            return list;
        }

         // DELETE api/Asientos/
        [ResponseType(typeof(Seccion))]
        public Seccion DeleteEventoSeccion(int id)
        {
            var list = from asient in db.Asientoes where asient.SeccionId == id select asient;
            var eliminarDetail = from dt in db.Evento_Seccion where dt.SeccionId == id select dt;

            foreach (var detail in eliminarDetail)
            {
                db.Evento_Seccion.Remove(detail);
            }


            foreach (var asiento in list)
            {
                db.Asientoes.Remove(asiento);
            }

            Seccion seccionToDelete= db.Seccions.Find(id);
            if (seccionToDelete == null)
            {
                return null;
            }
            db.Seccions.Remove(seccionToDelete);
            db.SaveChangesAsync();

            return seccionToDelete; 
        }

    




    }
}
