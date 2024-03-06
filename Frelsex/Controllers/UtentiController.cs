using Frelsex.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Frelsex.Controllers
{
    public class UtentiController : Controller
    {

        private readonly FrelsexDbContext db = new FrelsexDbContext();

        // GET: Utenti
        public ActionResult Index()
        {
            IQueryable<Utente> utenti = db.Utenti.Include(u => u.Cliente);
            return View(utenti.ToList());
        }


        // GET: Clienti/Create
        [Authorize(Roles = "Utente")] // Assicurati che questo sia il nome del ruolo corretto
        public ActionResult Create()
        {
            // Qui presupponiamo che l'ID dell'utente sia lo stesso ID associato al ruolo di cliente
            int userId = Convert.ToInt32(User.Identity.GetUserId()); // Utilizza il metodo appropriato per ottenere l'ID dell'utente loggato
            var model = new Cliente { ID = userId };
            return View(model);
        }

        // POST: Clienti/Create
        [HttpPost]
        [Authorize(Roles = "Utente")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,CodiceFiscale,PartitaIVA,IsAzienda")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(User.Identity.GetUserId());
                cliente.ID = userId; // Imposta l'ID dell'utente loggato come UtenteID del Cliente
                db.Clienti.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Utenti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utenti.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        // GET: Utenti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utenti.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,Email")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                var utenteDaAggiornare = db.Utenti.Find(utente.ID);
                if (utenteDaAggiornare != null)
                {
                    utenteDaAggiornare.Username = utente.Username;
                    utenteDaAggiornare.Password = utente.Password; // Considera l'hashing della password
                    utenteDaAggiornare.Email = utente.Email;

                    // Non aggiornare RuoloID o IsAdmin qui, poiché non dovrebbero essere modificati

                    db.Entry(utenteDaAggiornare).State = EntityState.Modified;
                    db.SaveChanges(); // Questo salva effettivamente le modifiche nel database
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return View(utente);
        }


        // GET: Utenti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utenti.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        // POST: Utenti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utente utente = db.Utenti.Find(id);
            if (utente != null)
            {
                db.Utenti.Remove(utente);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
