using BusinessDomain;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiamiJobsFinder.Controllers
{
    public class SearchController : Controller
    {

        MiamiJobsFinderDb _db = new MiamiJobsFinderDb();

        // GET: Search
        public ActionResult Index(string searchTerm = null)
        {
            var model = from r in _db.JobOffers
                        where (searchTerm == null || r.Title.StartsWith(searchTerm))
                        orderby r.IssuedDate
                        select new JobOfferListViewModel
                        {
                            Id = r.Id,
                            Title = r.Title,
                            IssuedDate = r.IssuedDate,
                            ExpirationDate = r.ExpirationDate,
                            Description = r.Description,
                            ContactPerson = r.ContactPerson,
                            Location = r.Location

                        };

            //var model = _db.JobOffers
            //    .OrderBy(r => r.IssuedDate)
            //    .Where(r => searchTerm == null || r.Title.StartsWith(searchTerm))
            //    .Take(10);
            //    //.Select (new JobOfferListViewModel
            //{
            //    Id = r.Id,
            //    Title = r.Title,
            //    IssuedDate = r.IssuedDate,
            //    ExpirationDate= r.ExpirationDate,
            //    Description= r.Description,
            //    ContactPerson= r.ContactPerson,
            //    City = (from l in _db.Locations where l.Id == r.LocationId select l.City)

            //});

            return View(model);
        }

        // GET: Search/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
