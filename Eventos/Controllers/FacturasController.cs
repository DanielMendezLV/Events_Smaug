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
    public class FacturasController : ApiController
    {
        private EventosContext db = new EventosContext();

        // GET api/Facturas
        public IQueryable<Factura> GetFacturas()
        {
            return db.Facturas;
        }

        // GET api/Facturas/5
        [ResponseType(typeof(Factura))]
        public async Task<IHttpActionResult> GetFactura(int id)
        {
            Factura factura = await db.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        // PUT api/Facturas/5
        public async Task<IHttpActionResult> PutFactura(int id, Factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != factura.IdFactura)
            {
                return BadRequest();
            }

            db.Entry(factura).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
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

        // POST api/Facturas
        [ResponseType(typeof(Factura))]
        public async Task<IHttpActionResult> PostFactura(Factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Facturas.Add(factura);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = factura.IdFactura }, factura);
        }

        // DELETE api/Facturas/5
        [ResponseType(typeof(Factura))]
        public async Task<IHttpActionResult> DeleteFactura(int id)
        {
            Factura factura = await db.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            db.Facturas.Remove(factura);
            await db.SaveChangesAsync();

            return Ok(factura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FacturaExists(int id)
        {
            return db.Facturas.Count(e => e.IdFactura == id) > 0;
        }
    }
}