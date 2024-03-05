using Frelsex.Models;
using System.Web.Mvc;

namespace Frelsex.Controllers
{
    public class HomeController : Controller
    {
        public readonly MyRole roleProvider = new MyRole();
        public ActionResult Index(string username)
        {
            var roles = roleProvider.GetRolesForUser(username);
            if (roles == null)
            {
                TempData["ErrorMessage"] = "Si è verificato un errore durante il recupero dei ruoli dell'utente.";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}