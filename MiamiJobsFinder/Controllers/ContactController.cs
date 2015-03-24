using System.ComponentModel;
using System.Web.Mvc;

namespace MiamiJobsFinder.Controllers
{
    [DisplayName("Contact")]
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
    }
}