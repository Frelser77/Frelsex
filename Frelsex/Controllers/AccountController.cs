using Frelsex.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Frelsex.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(MyLogin model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (FrelsexDbContext db = new FrelsexDbContext())
                {
                    Utente user = db.Utenti.FirstOrDefault(u => (u.Username == model.UsernameOrEmail || u.Email == model.UsernameOrEmail)
                                                        && u.Password == model.Password);

                    if (user != null)
                    {
                        // Ottieni i ruoli per l'utente
                        string[] roles = db.Ruoli.Where(r => r.Utenti.Any(u => u.ID == user.ID)).Select(r => r.Nome).ToArray();

                        // Crea il ticket di autenticazione
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                            1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(20), false, string.Join(",", roles));


                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        HttpContext.Response.Cookies.Add(authCookie);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tentativo di accesso non valido.");
                    }
                }
            }

            // Se siamo arrivati fin qui, qualcosa è fallito, quindi ri-mostra il form
            return View(model);
        }

        // Metodo per il Logout
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }




        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


    }
}