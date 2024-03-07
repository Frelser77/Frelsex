using Frelsex.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Frelsex.Controllers
{

    public class AggiornamentiSpedizioneController : Controller
    {
        private readonly FrelsexDbContext db = new FrelsexDbContext();

        // GET: AggiornamentiSpedizione
        [Authorize(Roles = "Utente, Admin")]
        public ActionResult Index()
        {
            IQueryable<AggiornamentoSpedizione> aggiornamentiSpedizione = db.AggiornamentiSpedizione.Include(a => a.Spedizione);
            return View(aggiornamentiSpedizione.ToList());
        }

        // GET: AggiornamentiSpedizione/Details/5
        [Authorize(Roles = "Utente, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spedizione spedizione = db.Spedizioni.Include(s => s.AggiornamentiSpedizione) // Assicurati che la tua entità Spedizione abbia una proprietà di navigazione che punti agli aggiornamenti
                                   .SingleOrDefault(s => s.ID == id);
            if (spedizione == null)
            {
                return HttpNotFound();
            }
            return View(spedizione);
        }


        // GET: AggiornamentiSpedizione/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.IDSpedizione = new SelectList(db.Spedizioni, "ID", "NumeroIdentificativo");
            return View();
        }

        // POST: AggiornamentiSpedizione/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDSpedizione,Stato,Luogo,Descrizione,DataOraAggiornamento")] AggiornamentoSpedizione aggiornamentoSpedizione)
        {
            if (ModelState.IsValid)
            {
                db.AggiornamentiSpedizione.Add(aggiornamentoSpedizione);

                // Aggiornamento dello stato della spedizione associata
                Spedizione spedizione = db.Spedizioni.Find(aggiornamentoSpedizione.IDSpedizione);
                if (spedizione != null)
                {
                    spedizione.Stato = aggiornamentoSpedizione.Stato; // Assicurati che il campo Stato in Spedizione sia di tipo stringa o adattalo secondo il tuo modello
                    db.Entry(spedizione).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDSpedizione = new SelectList(db.Spedizioni, "ID", "NumeroIdentificativo", aggiornamentoSpedizione.IDSpedizione);
            return View(aggiornamentoSpedizione);
        }


        // GET: AggiornamentiSpedizione/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AggiornamentoSpedizione aggiornamentoSpedizione = db.AggiornamentiSpedizione.Find(id);
            if (aggiornamentoSpedizione == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDSpedizione = new SelectList(db.Spedizioni, "ID", "NumeroIdentificativo", aggiornamentoSpedizione.IDSpedizione);
            return View(aggiornamentoSpedizione);
        }

        // POST: AggiornamentiSpedizione/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDSpedizione,Stato,Luogo,Descrizione,DataOraAggiornamento")] AggiornamentoSpedizione aggiornamentoSpedizione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aggiornamentoSpedizione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDSpedizione = new SelectList(db.Spedizioni, "ID", "NumeroIdentificativo", aggiornamentoSpedizione.IDSpedizione);
            return View(aggiornamentoSpedizione);
        }

        // GET: AggiornamentiSpedizione/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AggiornamentoSpedizione aggiornamentoSpedizione = db.AggiornamentiSpedizione.Find(id);
            if (aggiornamentoSpedizione == null)
            {
                return HttpNotFound();
            }
            return View(aggiornamentoSpedizione);
        }

        // POST: AggiornamentiSpedizione/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AggiornamentoSpedizione aggiornamentoSpedizione = db.AggiornamentiSpedizione.Find(id);
            db.AggiornamentiSpedizione.Remove(aggiornamentoSpedizione);
            db.SaveChanges();
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