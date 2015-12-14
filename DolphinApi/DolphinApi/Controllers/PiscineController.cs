using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DolphinApi.Models;

namespace DolphinApi.Controllers
{
    public class PiscineController : ApiController
    {
        private DolphinEntities db = new DolphinEntities();

        // GET: api/Piscine
        public IEnumerable<piscine> Getpiscine()
        {
            return db.piscine.ToList();
        }

        // GET: api/Piscine/5
        [ResponseType(typeof(piscine))]
        public IHttpActionResult Getpiscine(int id)
        {
            piscine piscine = db.piscine.Find(id);
            if (piscine == null)
            {
                return NotFound();
            }

            return Ok(piscine);
        }

        // PUT: api/Piscine/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpiscine(int id, piscine piscine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != piscine.ID_PISCINE)
            {
                return BadRequest();
            }

            db.Entry(piscine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!piscineExists(id))
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

        // POST: api/Piscine
        [ResponseType(typeof(piscine))]
        public IHttpActionResult Postpiscine(piscine piscine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.piscine.Add(piscine);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = piscine.ID_PISCINE }, piscine);
        }

        // DELETE: api/Piscine/5
        [ResponseType(typeof(piscine))]
        public IHttpActionResult Deletepiscine(int id)
        {
            piscine piscine = db.piscine.Find(id);
            if (piscine == null)
            {
                return NotFound();
            }

            db.piscine.Remove(piscine);
            db.SaveChanges();

            return Ok(piscine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool piscineExists(int id)
        {
            return db.piscine.Count(e => e.ID_PISCINE == id) > 0;
        }
    }
}