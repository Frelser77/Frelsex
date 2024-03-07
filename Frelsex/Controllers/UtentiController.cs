using Frelsex.Models;
using System.Data.Common;
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


        // GET: Utenti/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Verifica se l'utente attualmente loggato è un Admin
                Utente utenteLoggato = db.Utenti.FirstOrDefault(u => u.Username == User.Identity.Name);
                if (utenteLoggato == null || !utenteLoggato.IsAdmin)
                {
                    // Se l'utente non è un admin o non è trovato nel db, non è autorizzato a creare un nuovo utente
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Non sei autorizzato a creare nuovi utenti.");
                }
            }

            return View();
        }

        // POST: Utenti/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,Password,Email")] Utente utente)
        {
            string confermaPassword = Request.Form["ConfermaPassword"];

            if (ModelState.IsValid)
            {
                // Controllo password
                if (utente.Password != confermaPassword)
                {
                    ModelState.AddModelError("ConfermaPassword", "Le password non coincidono.");
                    return View(utente);
                }

                utente.IsAdmin = false;
                utente.RuoloID = 1;

                db.Utenti.Add(utente);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbException)
                {
                    ModelState.AddModelError("", "Si è verificato un errore durante la registrazione. Si prega di riprovare.");
                    return View(utente);
                }
            }

            return View(utente);
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
