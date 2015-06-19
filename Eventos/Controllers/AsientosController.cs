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
    public class AsientosController : ApiController
    {
        private EventosContext db = new EventosContext();

        // GET api/Asientos
        public IQueryable<Asiento> GetAsientoes()
        {
            return db.Asientoes;
        }

        // GET api/Asientos/5
        [ResponseType(typeof(Asiento))]
        public async Task<IHttpActionResult> GetAsiento(int id)
        {
            Asiento asiento = await db.Asientoes.FindAsync(id);
            if (asiento == null)
            {
                return NotFound();
            }

            return Ok(asiento);
        }

        // PUT api/Asientos/5
        public async Task<IHttpActionResult> PutAsiento(int id, Asiento asiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asiento.IdAsiento)
            {
                return BadRequest();
            }

            db.Entry(asiento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsientoExists(id))
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

        // POST api/Asientos
        [ResponseType(typeof(Asiento))]
        public async Task<IHttpActionResult> PostAsiento(Asiento asiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Asientoes.Add(asiento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = asiento.IdAsiento }, asiento);
        }

        // DELETE api/Asientos/5
        [ResponseType(typeof(Asiento))]
        public async Task<IHttpActionResult> DeleteAsiento(int id)
        {
            Asiento asiento = await db.Asientoes.FindAsync(id);
            if (asiento == null)
            {
                return NotFound();
            }

            db.Asientoes.Remove(asiento);
            await db.SaveChangesAsync();

            return Ok(asiento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsientoExists(int id)
        {
            return db.Asientoes.Count(e => e.IdAsiento == id) > 0;
        }
    }
}