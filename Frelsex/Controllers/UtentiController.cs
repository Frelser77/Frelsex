using Frelsex.Models;
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

        /*
        [NonAction]
        // GET: Utenti/Create
        public ActionResult Create()
        {

            return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "La creazione non è permessa da qui.");
        }
        */

        // POST: Utenti/Create
        [NonAction]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,Password,Email,IsAdmin")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                db.Utenti.Add(utente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(utente);

            /*return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "La creazione non è permessa da qui.");*/
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

        // POST: Utenti/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,Email,IsAdmin")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                // Applica la logica di hashing alla password, se modificata.
                db.Entry(utente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utente);
        }

        [NonAction]
        public ActionResult Delete(int? id)
        {
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "La cancellazione non è permessa da qui.");
        }


        [NonAction]
        public ActionResult DeleteConfirmed(int id)
        {
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "La cancellazione non è permessa da qui.");
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
