using System.Web.Mvc;
using BDL;

namespace MiamiJobsFinder.Controllers
{
    [Authorize(Roles = "admin")]
    public class SendEmailController : Controller
    {
        // GET: SendEmail
        public ActionResult Index()
        {
            return View();
        }
    }
}