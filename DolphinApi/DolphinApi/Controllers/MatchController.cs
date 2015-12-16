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
    public class MatchController : ApiController
    {
        private DolphinEntities db = new DolphinEntities();

        // GET: api/Match
        public IEnumerable<dolphinmatch> Getdolphinmatch()
        {
            return db.dolphinmatch.ToList();
        }

        // GET: api/Match/5
        [ResponseType(typeof(dolphinmatch))]
        public IHttpActionResult Getdolphinmatch(int id)
        {
            dolphinmatch dolphinmatch = db.dolphinmatch.Find(id);
            if (dolphinmatch == null)
            {
                return NotFound();
            }

            return Ok(dolphinmatch);
        }

        // GET: api/Match?idUtilisateur=...
        [ResponseType(typeof(dolphinmatch))]
        public IEnumerable<dolphinmatch> Getdolphinmatch(string idUtilisateur)
        {
            try {
                int id = Convert.ToInt32(idUtilisateur);
                IEnumerable<dolphinmatch> dolphinmatch = db.dolphinmatch.Where(m => m.ID_UTILISATEUR == id).ToList();
                if (dolphinmatch == null)
                {
                    return null;
                }

                return dolphinmatch;
            } catch
            {
                return null;
            }
        }

        // PUT: api/Match/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdolphinmatch(int id, dolphinmatch dolphinmatch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dolphinmatch.ID_MATCH)
            {
                return BadRequest();
            }

            db.Entry(dolphinmatch).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!dolphinmatchExists(id))
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

        // POST: api/Match
        [ResponseType(typeof(dolphinmatch))]
        public IHttpActionResult Postdolphinmatch(dolphinmatch dolphinmatch)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.dolphinmatch.Add(dolphinmatch);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = dolphinmatch.ID_MATCH }, dolphinmatch);
            }
            catch (Exception ex)
            {

                string message = "";
                var innerEx = ex;
                while (innerEx != null)
                {
                    message = message + innerEx.Message;
                    innerEx = innerEx.InnerException;
                }

                return BadRequest(message);
            }
        }

        // DELETE: api/Match/5
        [ResponseType(typeof(dolphinmatch))]
        public IHttpActionResult Deletedolphinmatch(int id)
        {
            dolphinmatch dolphinmatch = db.dolphinmatch.Find(id);
            if (dolphinmatch == null)
            {
                return NotFound();
            }

            db.dolphinmatch.Remove(dolphinmatch);
            db.SaveChanges();

            return Ok(dolphinmatch);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool dolphinmatchExists(int id)
        {
            return db.dolphinmatch.Count(e => e.ID_MATCH == id) > 0;
        }
    }
}