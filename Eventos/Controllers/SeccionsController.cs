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
    public class SeccionsController : ApiController
    {
        private EventosContext db = new EventosContext();

        // GET: api/Seccions
        public IQueryable<Seccion> GetSeccions()
        {
            return db.Seccions;
        }

        // GET: api/Seccions/5
        [ResponseType(typeof(Seccion))]
        public async Task<IHttpActionResult> GetSeccion(int id)
        {
            Seccion seccion = await db.Seccions.FindAsync(id);
            if (seccion == null)
            {
                return NotFound();
            }

            return Ok(seccion);
        }

        // PUT: api/Seccions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSeccion(int id, Seccion seccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seccion.IdSeccion)
            {
                return BadRequest();
            }

            db.Entry(seccion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeccionExists(id))
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

        // POST: api/Seccions
        [ResponseType(typeof(Seccion))]
        public async Task<IHttpActionResult> PostSeccion(Seccion seccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Seccions.Add(seccion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = seccion.IdSeccion }, seccion);
        }

        // DELETE: api/Seccions/5
        [ResponseType(typeof(Seccion))]
        public async Task<IHttpActionResult> DeleteSeccion(int id)
        {
            Seccion seccion = await db.Seccions.FindAsync(id);
            if (seccion == null)
            {
                return NotFound();
            }

            db.Seccions.Remove(seccion);
            await db.SaveChangesAsync();

            return Ok(seccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeccionExists(int id)
        {
            return db.Seccions.Count(e => e.IdSeccion == id) > 0;
        }
    }
}