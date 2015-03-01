using System.Web.Mvc;
using BDL;


namespace MiamiJobsFinder.Controllers
{
    [Authorize(Roles = "admin")]
    public class SendEmailController : Controller
    {
        private readonly ISendJobOffersService sendJobOffersService;

        public SendEmailController(ISendJobOffersService sendJobOffersService)
        {
            this.sendJobOffersService = sendJobOffersService;
        }

        // GET: SendEmail
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Send()
        {
            sendJobOffersService.SendJobOffers();
            return RedirectToAction("Index", "Home" );
        }
    }
}