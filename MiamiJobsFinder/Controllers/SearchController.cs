using System.ComponentModel;
using BusinessDomain;
using DAL;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace MiamiJobsFinder.Controllers
{
    [DisplayName("Search")]
    public class SearchController : Controller
    {

        MiamiJobsFinderDb _db = new MiamiJobsFinderDb();


        public ActionResult Autocomplete(string term)
        {
            var model = _db.JobOffers
                        .Where(r => r.Title.StartsWith(term))
                        .Take(10)
                        .Select(r => new
                        {
                            label = r.Title
                        });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Search
        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            var model = _db.JobOffers
                .OrderBy(r => r.IssuedDate)
                .Where(r => searchTerm == null || r.Title.StartsWith(searchTerm))
                .Select(r => new JobOfferListViewModel
                        {
                            Id = r.Id,
                            Title = r.Title,
                            IssuedDate = r.IssuedDate,
                            ExpirationDate = r.ExpirationDate,
                            Description = r.Description,
                            ContactPerson = r.ContactPerson,
                            Location = r.Location

                        }).ToPagedList(page, 10);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Jobs", model);
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
