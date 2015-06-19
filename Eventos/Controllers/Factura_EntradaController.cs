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
    public class Factura_EntradaController : ApiController
    {
        private EventosContext db = new EventosContext();

        // GET api/Factura_Entrada
        public IQueryable<Factura_Entrada> GetFactura_Entrada()
        {
            return db.Factura_Entrada;
        }

        // GET api/Factura_Entrada/5
        [ResponseType(typeof(Factura_Entrada))]
        public async Task<IHttpActionResult> GetFactura_Entrada(int id)
        {
            Factura_Entrada factura_entrada = await db.Factura_Entrada.FindAsync(id);
            if (factura_entrada == null)
            {
                return NotFound();
            }

            return Ok(factura_entrada);
        }

        // PUT api/Factura_Entrada/5
        public async Task<IHttpActionResult> PutFactura_Entrada(int id, Factura_Entrada factura_entrada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != factura_entrada.Id)
            {
                return BadRequest();
            }

            db.Entry(factura_entrada).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Factura_EntradaExists(id))
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

        // POST api/Factura_Entrada
        [ResponseType(typeof(Factura_Entrada))]
        public async Task<IHttpActionResult> PostFactura_Entrada(Factura_Entrada factura_entrada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Factura_Entrada.Add(factura_entrada);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = factura_entrada.Id }, factura_entrada);
        }

        // DELETE api/Factura_Entrada/5
        [ResponseType(typeof(Factura_Entrada))]
        public async Task<IHttpActionResult> DeleteFactura_Entrada(int id)
        {
            Factura_Entrada factura_entrada = await db.Factura_Entrada.FindAsync(id);
            if (factura_entrada == null)
            {
                return NotFound();
            }

            db.Factura_Entrada.Remove(factura_entrada);
            await db.SaveChangesAsync();

            return Ok(factura_entrada);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Factura_EntradaExists(int id)
        {
            return db.Factura_Entrada.Count(e => e.Id == id) > 0;
        }
    }
}