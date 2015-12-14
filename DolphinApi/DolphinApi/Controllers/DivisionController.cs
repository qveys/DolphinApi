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
    public class DivisionController : ApiController
    {
        private DolphinEntities db = new DolphinEntities();

        // GET: api/Division
        public IEnumerable<division> Getdivision()
        {
            return db.division.ToList();
        }

        // GET: api/Division/5
        [ResponseType(typeof(division))]
        public IHttpActionResult Getdivision(int id)
        {
            division division = db.division.Find(id);
            if (division == null)
            {
                return NotFound();
            }

            return Ok(division);
        }

        // PUT: api/Division/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdivision(int id, division division)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != division.ID_DIVISION)
            {
                return BadRequest();
            }

            db.Entry(division).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!divisionExists(id))
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

        // POST: api/Division
        [ResponseType(typeof(division))]
        public IHttpActionResult Postdivision(division division)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.division.Add(division);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = division.ID_DIVISION }, division);
        }

        // DELETE: api/Division/5
        [ResponseType(typeof(division))]
        public IHttpActionResult Deletedivision(int id)
        {
            division division = db.division.Find(id);
            if (division == null)
            {
                return NotFound();
            }

            db.division.Remove(division);
            db.SaveChanges();

            return Ok(division);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool divisionExists(int id)
        {
            return db.division.Count(e => e.ID_DIVISION == id) > 0;
        }
    }
}