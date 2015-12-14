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
    public class UtilisateurController : ApiController
    {
        private DolphinEntities db = new DolphinEntities();

        // GET: api/Utilisateur
        public IEnumerable<utilisateur> Getutilisateur()
        {
            return db.utilisateur.ToList();
        }

        // GET: api/Utilisateur/5
        [ResponseType(typeof(utilisateur))]
        public IHttpActionResult Getutilisateur(int id)
        {
            utilisateur utilisateur = db.utilisateur.Find(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return Ok(utilisateur);
        }

        // GET: api/Utilisateur?login=...
        [ResponseType(typeof(utilisateur))]
        public IHttpActionResult Getutilisateur(string login)
        {
            utilisateur utilisateur = db.utilisateur.Where(u => u.LOGIN == login).SingleOrDefault();
            if (utilisateur == null)
            {
                return NotFound();
            }

            return Ok(utilisateur);
        }



        // PUT: api/Utilisateur/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Pututilisateur(int id, utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != utilisateur.ID_UTILISATEUR)
            {
                return BadRequest();
            }

            db.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!utilisateurExists(id))
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

        // POST: api/Utilisateur
        [ResponseType(typeof(utilisateur))]
        public IHttpActionResult Postutilisateur(utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.utilisateur.Add(utilisateur);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = utilisateur.ID_UTILISATEUR }, utilisateur);
        }

        // DELETE: api/Utilisateur/5
        [ResponseType(typeof(utilisateur))]
        public IHttpActionResult Deleteutilisateur(int id)
        {
            utilisateur utilisateur = db.utilisateur.Find(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            db.utilisateur.Remove(utilisateur);
            db.SaveChanges();

            return Ok(utilisateur);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool utilisateurExists(int id)
        {
            return db.utilisateur.Count(e => e.ID_UTILISATEUR == id) > 0;
        }
    }
}