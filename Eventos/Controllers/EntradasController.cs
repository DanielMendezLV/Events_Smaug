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
    public class EntradasController : ApiController
    {
        private EventosContext db = new EventosContext();

        // GET api/Entradas
        public IQueryable<Entrada> GetEntradas()
        {
            return db.Entradas;
        }

        // GET api/Entradas/5
        [ResponseType(typeof(Entrada))]
        public async Task<IHttpActionResult> GetEntrada(int id)
        {
            Entrada entrada = await db.Entradas.FindAsync(id);
            if (entrada == null)
            {
                return NotFound();
            }

            return Ok(entrada);
        }

        // PUT api/Entradas/5
        public async Task<IHttpActionResult> PutEntrada(int id, Entrada entrada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entrada.IdEntrada)
            {
                return BadRequest();
            }

            db.Entry(entrada).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntradaExists(id))
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

        // POST api/Entradas
        [ResponseType(typeof(Entrada))]
        public async Task<IHttpActionResult> PostEntrada(Entrada entrada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entradas.Add(entrada);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = entrada.IdEntrada }, entrada);
        }

        // DELETE api/Entradas/5
        [ResponseType(typeof(Entrada))]
        public async Task<IHttpActionResult> DeleteEntrada(int id)
        {
            Entrada entrada = await db.Entradas.FindAsync(id);
            if (entrada == null)
            {
                return NotFound();
            }

            db.Entradas.Remove(entrada);
            await db.SaveChangesAsync();

            return Ok(entrada);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntradaExists(int id)
        {
            return db.Entradas.Count(e => e.IdEntrada == id) > 0;
        }
    }
}