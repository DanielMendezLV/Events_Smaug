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
    public class Evento_SeccionController : ApiController
    {
        private EventosContext db = new EventosContext();

        // GET: api/Evento_Seccion
        public IQueryable<Evento_Seccion> GetEvento_Seccion()
        {
            return db.Evento_Seccion;
        }

        // GET: api/Evento_Seccion/5
        [ResponseType(typeof(Evento_Seccion))]
        public async Task<IHttpActionResult> GetEvento_Seccion(int id)
        {
            Evento_Seccion evento_Seccion = await db.Evento_Seccion.FindAsync(id);
            if (evento_Seccion == null)
            {
                return NotFound();
            }

            return Ok(evento_Seccion);
        }

        // PUT: api/Evento_Seccion/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEvento_Seccion(int id, Evento_Seccion evento_Seccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != evento_Seccion.EventoId)
            {
                return BadRequest();
            }

            db.Entry(evento_Seccion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Evento_SeccionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Evento_Seccion
        [ResponseType(typeof(Evento_Seccion))]
        public async Task<IHttpActionResult> PostEvento_Seccion(Evento_Seccion evento_Seccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Evento_Seccion.Add(evento_Seccion);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Evento_SeccionExists(evento_Seccion.EventoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = evento_Seccion.EventoId }, evento_Seccion);
        }

        // DELETE: api/Evento_Seccion/5
        [ResponseType(typeof(Evento_Seccion))]
        public async Task<IHttpActionResult> DeleteEvento_Seccion(int id)
        {
            Evento_Seccion evento_Seccion = await db.Evento_Seccion.FindAsync(id);
            if (evento_Seccion == null)
            {
                return NotFound();
            }

            db.Evento_Seccion.Remove(evento_Seccion);
            await db.SaveChangesAsync();

            return Ok(evento_Seccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Evento_SeccionExists(int id)
        {
            return db.Evento_Seccion.Count(e => e.EventoId == id) > 0;
        }
    }
}