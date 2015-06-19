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
    public class TipoesController : ApiController
    {
        private EventosContext db = new EventosContext();

        // GET: api/Tipoes
        public IQueryable<Tipo> GetTipoes()
        {
            return db.Tipoes;
        }

        // GET: api/Tipoes/5
        [ResponseType(typeof(Tipo))]
        public async Task<IHttpActionResult> GetTipo(int id)
        {
            Tipo tipo = await db.Tipoes.FindAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(tipo);
        }

        // PUT: api/Tipoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTipo(int id, Tipo tipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipo.IdTipo)
            {
                return BadRequest();
            }

            db.Entry(tipo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoExists(id))
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

        // POST: api/Tipoes
        [ResponseType(typeof(Tipo))]
        public async Task<IHttpActionResult> PostTipo(Tipo tipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tipoes.Add(tipo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipo.IdTipo }, tipo);
        }

        // DELETE: api/Tipoes/5
        [ResponseType(typeof(Tipo))]
        public async Task<IHttpActionResult> DeleteTipo(int id)
        {
            Tipo tipo = await db.Tipoes.FindAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }

            db.Tipoes.Remove(tipo);
            await db.SaveChangesAsync();

            return Ok(tipo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoExists(int id)
        {
            return db.Tipoes.Count(e => e.IdTipo == id) > 0;
        }
    }
}