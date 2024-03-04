using Frelsex.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Frelsex.Controllers
{
    public class SpedizioniController : Controller
    {
        private readonly FrelsexDbContext db = new FrelsexDbContext();

        // GET: Spedizioni
        public ActionResult Index()
        {
            IQueryable<Spedizione> spedizioni = db.Spedizioni.Include(s => s.Cliente);
            return View(spedizioni.ToList());
        }

        // GET: Spedizioni/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spedizione spedizione = db.Spedizioni.Find(id);
            if (spedizione == null)
            {
                return HttpNotFound();
            }
            return View(spedizione);
        }

        // GET: Spedizioni/Create
        public ActionResult Create()
        {
            ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Nome");
            return View(new Spedizione());
        }

        // POST: Spedizioni/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCliente,Peso,CittàDestinataria,IndirizzoDestinatario,NominativoDestinatario")] Spedizione spedizione)
        {
            if (ModelState.IsValid)
            {
                spedizione.NumeroIdentificativo = GeneraNumeroIdentificativo();
                spedizione.CostoSpedizione = CalcolaCostoSpedizione(spedizione.Peso);
                spedizione.DataSpedizione = DateTime.Today;
                spedizione.DataConsegnaPrevista = DateTime.Today.AddDays(3);
                spedizione.Stato = "In Transito";

                db.Spedizioni.Add(spedizione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Nome", spedizione.IDCliente);
            return View(spedizione);
        }

        private string GeneraNumeroIdentificativo()
        {
            return Guid.NewGuid().ToString("N");
        }

        private decimal CalcolaCostoSpedizione(decimal peso)
        {
            return 2 + (peso * 2) + ((int)(peso / 5) * 3);
        }



        // GET: Spedizioni/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spedizione spedizione = db.Spedizioni.Find(id);
            if (spedizione == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Nome", spedizione.IDCliente);
            return View(spedizione);
        }

        // POST: Spedizioni/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDCliente,NumeroIdentificativo,DataSpedizione,Peso,CittàDestinataria,IndirizzoDestinatario,NominativoDestinatario,CostoSpedizione,DataConsegnaPrevista,Stato")] Spedizione spedizione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spedizione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Nome", spedizione.IDCliente);
            return View(spedizione);
        }

        // GET: Spedizioni/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spedizione spedizione = db.Spedizioni.Find(id);
            if (spedizione == null)
            {
                return HttpNotFound();
            }
            return View(spedizione);
        }

        // POST: Spedizioni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spedizione spedizione = db.Spedizioni.Find(id);
            db.Spedizioni.Remove(spedizione);
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