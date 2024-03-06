﻿using Frelsex.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Frelsex.Controllers
{
    /*[Authorize(Roles = "Utente,Admin")]*/
    public class ClientiController : Controller
    {
        private readonly FrelsexDbContext db = new FrelsexDbContext();

        // GET: Clienti
        public ActionResult Index()
        {
            return View(db.Clienti.ToList());
        }

        // GET: Clienti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clienti.Include(c => c.Spedizioni) // Include le spedizioni se necessario
                                        .SingleOrDefault(c => c.ID == id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clienti/Create
        public ActionResult Create()
        {
            // Prepara eventuali ViewBag per dropdown (se necessario)
            ViewBag.IsAzienda = new SelectList(new[] { new { Value = false, Text = "Azienda" }, new { Value = true, Text = "Privato" } }, "Value", "Text");

            return View();
        }

        // POST: Clienti/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,CodiceFiscale,PartitaIVA,IsAzienda")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clienti.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clienti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clienti.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            // Prepara eventuali ViewBag per dropdown (se necessario)
            return View(cliente);
        }

        // POST: Clienti/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,CodiceFiscale,PartitaIVA,IsAzienda")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clienti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clienti.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clienti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clienti.Find(id);
            // Aggiungi qui la logica per gestire le dipendenze, se necessario
            db.Clienti.Remove(cliente);
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                // Gestisci l'errore (es. mostrando un messaggio all'utente)
                return RedirectToAction("DeleteFailed");
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
