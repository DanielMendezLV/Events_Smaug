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
    public class LugarsController : ApiController
    {
        private EventosContext db = new EventosContext();

        // GET: api/Lugars
        public IQueryable<Lugar> GetLugars()
        {
            return db.Lugars;
        }

        // GET: api/Lugars/5
        [ResponseType(typeof(Lugar))]
        public async Task<IHttpActionResult> GetLugar(int id)
        {
            Lugar lugar = await db.Lugars.FindAsync(id);
            if (lugar == null)
            {
                return NotFound();
            }

            return Ok(lugar);
        }

        // PUT: api/Lugars/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLugar(int id, Lugar lugar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lugar.IdLugar)
            {
                return BadRequest();
            }

            db.Entry(lugar).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LugarExists(id))
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

        // POST: api/Lugars
        [ResponseType(typeof(Lugar))]
        public async Task<IHttpActionResult> PostLugar(Lugar lugar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lugars.Add(lugar);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = lugar.IdLugar }, lugar);
        }

        // DELETE: api/Lugars/5
        [ResponseType(typeof(Lugar))]
        public async Task<IHttpActionResult> DeleteLugar(int id)
        {
            Lugar lugar = await db.Lugars.FindAsync(id);
            if (lugar == null)
            {
                return NotFound();
            }

            db.Lugars.Remove(lugar);
            await db.SaveChangesAsync();

            return Ok(lugar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LugarExists(int id)
        {
            return db.Lugars.Count(e => e.IdLugar == id) > 0;
        }
    }
}