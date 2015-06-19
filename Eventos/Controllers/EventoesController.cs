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
    public class EventoesController : ApiController
    {
        private EventosContext db = new EventosContext();

        // GET: api/Eventoes
        public IQueryable<Evento> GetEventoes()
        {
            return db.Eventoes;
        }

        // GET: api/Eventoes/5
        [ResponseType(typeof(Evento))]
        public async Task<IHttpActionResult> GetEvento(int id)
        {
            Evento evento = await db.Eventoes.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        // PUT: api/Eventoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEvento(int id, Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != evento.IdEvento)
            {
                return BadRequest();
            }

            db.Entry(evento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
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

        // POST: api/Eventoes
        [ResponseType(typeof(Evento))]
        public async Task<IHttpActionResult> PostEvento(Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Eventoes.Add(evento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = evento.IdEvento }, evento);
        }

        // DELETE: api/Eventoes/5
        [ResponseType(typeof(Evento))]
        public async Task<IHttpActionResult> DeleteEvento(int id)
        {
            Evento evento = await db.Eventoes.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            db.Eventoes.Remove(evento);
            await db.SaveChangesAsync();

            return Ok(evento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventoExists(int id)
        {
            return db.Eventoes.Count(e => e.IdEvento == id) > 0;
        }
    }
}