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
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            IQueryable<Spedizione> spedizioni = db.Spedizioni.Include(s => s.Cliente);
            return View(spedizioni.ToList());
        }

        // GET: Spedizioni/Details/5
        [Authorize(Roles = "Utente,Admin")]
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
        [Authorize]
        [Authorize(Roles = "Utente,Admin")]
        public ActionResult Create()
        {
            string username = User.Identity.Name;
            Utente utente = db.Utenti.SingleOrDefault(u => u.Username == username);

            if (utente?.Cliente_ID == null)
            {
                return RedirectToAction("Create", "Clienti");
            }

            Cliente cliente = db.Clienti.SingleOrDefault(c => c.ID == utente.Cliente_ID);

            if (cliente == null)
            {
                return RedirectToAction("Create", "Clienti");
            }

            ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Nome", cliente.ID);

            return View(new Spedizione
            {
                DataSpedizione = DateTime.Today,
                DataConsegnaPrevista = DateTime.Today.AddDays(3),
                IDCliente = cliente.ID,
                NumeroIdentificativo = GeneraNumeroIdentificativo(),
                Stato = StatoSpedizione.inTransito.ToString()
            });
        }



        // POST: Spedizioni/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Utente,Admin")]
        public ActionResult Create([Bind(Exclude = "NumeroIdentificativo,Stato")] Spedizione spedizione)
        {
            spedizione.NumeroIdentificativo = GeneraNumeroIdentificativo();
            spedizione.CostoSpedizione = CalcolaCostoSpedizione(spedizione.Peso);
            spedizione.Stato = StatoSpedizione.inTransito.ToString();

            ModelState.Remove("NumeroIdentificativo");
            ModelState.Remove("Stato");

            if (ModelState.IsValid)
            {
                spedizione.DataSpedizione = DateTime.Now;
                spedizione.DataConsegnaPrevista = spedizione.DataSpedizione.AddDays(3);

                db.Spedizioni.Add(spedizione);
                db.SaveChanges();

                AggiornamentoSpedizione primoAggiornamento = new AggiornamentoSpedizione
                {
                    IDSpedizione = spedizione.ID,
                    Stato = StatoSpedizione.inTransito.ToString(),
                    Luogo = spedizione.CittàDestinataria,
                    Descrizione = $"Spedizione creata. Numero identificativo: {spedizione.NumeroIdentificativo}. Peso: {spedizione.Peso}. Indirizzo: {spedizione.IndirizzoDestinatario}.",
                    DataOraAggiornamento = DateTime.Now
                };

                db.AggiornamentiSpedizione.Add(primoAggiornamento);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Nome", spedizione.IDCliente);
            return View(spedizione);
        }

        [Authorize(Roles = "Utente")]
        private string GeneraNumeroIdentificativo()
        {
            return Guid.NewGuid().ToString("N");
        }
        [Authorize(Roles = "Utente")]
        private decimal CalcolaCostoSpedizione(decimal peso)
        {
            return 2 + (peso * 2) + ((int)(peso / 5) * 3);
        }

        // GET: Spedizioni/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,IDCliente,NumeroIdentificativo,DataSpedizione,Peso,CittàDestinataria,IndirizzoDestinatario,NominativoDestinatario,CostoSpedizione,DataConsegnaPrevista,Stato")] Spedizione spedizione)
        {
            if (ModelState.IsValid)
            {
                Spedizione originalSpedizione = db.Spedizioni.Include(s => s.AggiornamentiSpedizione).AsNoTracking().FirstOrDefault(s => s.ID == spedizione.ID);

                if (originalSpedizione != null && originalSpedizione.Stato != spedizione.Stato)
                {
                    AggiornamentoSpedizione nuovoAggiornamento = new AggiornamentoSpedizione
                    {
                        IDSpedizione = spedizione.ID,
                        Stato = spedizione.Stato,
                        Luogo = originalSpedizione.CittàDestinataria, // O qualsiasi altro luogo desiderato
                        Descrizione = "Lo stato della spedizione è stato aggiornato.",
                        DataOraAggiornamento = DateTime.Now
                    };

                    db.AggiornamentiSpedizione.Add(nuovoAggiornamento);
                }

                db.Entry(spedizione).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Nome", spedizione.IDCliente);
            return View(spedizione);
        }

        // GET: Spedizioni/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spedizione spedizione = db.Spedizioni.Find(id);
            db.Spedizioni.Remove(spedizione);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Utente,Admin")]
        public ActionResult SearchSpedizione()
        {
            var username = User.Identity.Name;
            var utente = db.Utenti.SingleOrDefault(u => u.Username == username);
            if (utente == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Utente non trovato.");
            }

            var cliente = db.Clienti.SingleOrDefault(c => c.ID == utente.Cliente_ID);
            if (cliente == null)
            {
                return HttpNotFound("Cliente non trovato.");
            }

            var codiceFiscaleOrPartitaIva = cliente.IsAzienda ? cliente.PartitaIVA : cliente.CodiceFiscale;

            var model = new SearchSpedizione { CodiceFiscaleOrPartitaIva = codiceFiscaleOrPartitaIva };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Utente,Admin")]
        public ActionResult SearchSpedizione(SearchSpedizione searchModel)
        {
            var spedizioni = db.Spedizioni.Include(s => s.Cliente)
                .Where(s => s.Cliente.CodiceFiscale == searchModel.CodiceFiscaleOrPartitaIva ||
                            s.Cliente.PartitaIVA == searchModel.CodiceFiscaleOrPartitaIva)
                .OrderByDescending(s => s.DataSpedizione)
                .ToList();

            if (spedizioni.Count == 0)
            {
                ViewBag.Message = "Nessuna spedizione trovata per i dati inseriti.";
                return View("SearchSpedizione", searchModel);
            }

            return View("SearchResults", spedizioni);
        }

        // chiamte async 

        [HttpGet]
        [Authorize(Roles = "Utente,Admin")]
        public ActionResult Dashboard()
        {
            // Qui puoi aggiungere qualsiasi logica di preparazione dei dati necessaria per la tua dashboard.
            // Per ora, restituiremo semplicemente la vista corrispondente.
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public JsonResult SpedizioniInConsegnaOggi()
        {
            var spedizioniOggi = db.Spedizioni
                .Where(s => DbFunctions.TruncateTime(s.DataConsegnaPrevista) == DbFunctions.TruncateTime(DateTime.Now)
                            && s.Stato == StatoSpedizione.inConsegna.ToString())
                .ToList();

            return Json(spedizioniOggi, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public JsonResult NumeroSpedizioniInAttesa()
        {
            var count = db.Spedizioni.Count(s => s.Stato == StatoSpedizione.inTransito.ToString());
            return Json(new { count = count }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public JsonResult SpedizioniPerCitta()
        {
            var gruppi = db.Spedizioni
                .GroupBy(s => s.CittàDestinataria)
                .Select(grp => new { Città = grp.Key, Count = grp.Count() })
                .ToList();

            return Json(gruppi, JsonRequestBehavior.AllowGet);
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