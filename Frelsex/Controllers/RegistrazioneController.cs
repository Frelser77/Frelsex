using Frelsex.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Frelsex.Controllers
{
    public class RegistrazioneController : Controller
    {
        private readonly FrelsexDbContext db = new FrelsexDbContext();

        // GET: Registrazione
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Utenti");
        }

        // GET: Registrazione/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Registrazione/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteUtenteViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Crea e aggiungi il Cliente
                        Cliente cliente = new Cliente
                        {
                            Nome = model.Nome,
                            CodiceFiscale = model.CodiceFiscale,
                            PartitaIVA = model.PartitaIVA,
                            IsAzienda = model.IsAzienda
                        };
                        db.Clienti.Add(cliente);
                        db.SaveChanges();

                        // Crea e aggiungi l'Utente associato al Cliente
                        Utente utente = new Utente
                        {
                            Username = model.Username,
                            Password = model.Password,
                            Email = model.Email,
                            IsAdmin = model.IsAdmin,
                            ClienteID = cliente.ID
                        };
                        db.Utenti.Add(utente);
                        db.SaveChanges(); // Completa la creazione delle entità

                        transaction.Commit();
                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        Console.WriteLine(ex.Message);

                        ModelState.AddModelError("", "Si è verificato un errore durante la creazione dell'utente e del cliente.");
                    }
                }
            }
            // Se ci arriviamo, qualcosa è andato storto, quindi ri-mostra la vista con il modello attuale
            return View(model);
        }

        // GET: Registrazione/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Utente utente = db.Utenti.Include(u => u.Cliente).FirstOrDefault(u => u.ID == id);
            if (utente == null || utente.Cliente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        // POST: Registrazione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utente utente = db.Utenti.Include(u => u.Cliente).FirstOrDefault(u => u.ID == id);

            if (utente == null)
            {
                return HttpNotFound();
            }

            // Verifico che l'utente abbia un cliente associato
            if (utente.ClienteID != null)
            {
                Cliente cliente = db.Clienti.Find(utente.ClienteID);
                if (cliente != null)
                {
                    db.Clienti.Remove(cliente); // Rimuovi il Cliente
                }
            }

            var utenteRuoli = db.UtentiRuoli.Where(ur => ur.UtenteID == id);
            foreach (var utenteRuolo in utenteRuoli)
            {
                db.UtentiRuoli.Remove(utenteRuolo); // Rimuovi i Ruoli associati all'utente
            }

            db.Utenti.Remove(utente); // Rimuovi l'utente
            db.SaveChanges(); // Salva tutto

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
