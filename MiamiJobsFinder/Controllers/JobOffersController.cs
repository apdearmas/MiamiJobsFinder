﻿using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using BusinessDomain;
using DAL;

namespace MiamiJobsFinder.Controllers
{
    public class JobOffersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobOffers
        public async Task<ActionResult> Index()
        {
            return View(await db.JobOffers.ToListAsync());
        }

        // GET: JobOffers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOffer jobOffer = await db.JobOffers.FindAsync(id);
            if (jobOffer == null)
            {
                return HttpNotFound();
            }
            return View(jobOffer);
        }

        // GET: JobOffers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,IssuedDate,ExpirationDate,Description")] JobOffer jobOffer)
        {
            if (ModelState.IsValid)
            {
                db.JobOffers.Add(jobOffer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(jobOffer);
        }

        // GET: JobOffers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOffer jobOffer = await db.JobOffers.FindAsync(id);
            if (jobOffer == null)
            {
                return HttpNotFound();
            }
            return View(jobOffer);
        }

        // POST: JobOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,IssuedDate,ExpirationDate,Description")] JobOffer jobOffer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobOffer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(jobOffer);
        }

        // GET: JobOffers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOffer jobOffer = await db.JobOffers.FindAsync(id);
            if (jobOffer == null)
            {
                return HttpNotFound();
            }
            return View(jobOffer);
        }

        // POST: JobOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            JobOffer jobOffer = await db.JobOffers.FindAsync(id);
            db.JobOffers.Remove(jobOffer);
            await db.SaveChangesAsync();
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
