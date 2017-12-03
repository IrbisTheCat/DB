using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    [Authorize(Roles = "moderator")]
    public class PerformanceController : Controller
    {
        private Entities db = new Entities();

        // GET: Performance
        public ActionResult Index()
        {
            var performances = db.Performances.Include(p => p.Band).Include(p => p.Location);
            return View(performances.ToList());
        }

        // GET: Performance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // GET: Performance/Create
        public ActionResult Create()
        {
            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName");
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "LocationName");
            return View();
        }

        // POST: Performance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BandId,LocationId,SetListId,Duriation,Date,Name")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.Performances.Add(performance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName", performance.BandId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "LocationName", performance.LocationId);
            return View(performance);
        }

        // GET: Performance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName", performance.BandId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "LocationName", performance.LocationId);
            return View(performance);
        }

        // POST: Performance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BandId,LocationId,SetListId,Duriation,Date,Name")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(performance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName", performance.BandId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "LocationName", performance.LocationId);
            return View(performance);
        }

        // GET: Performance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // POST: Performance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Performance performance = db.Performances.Find(id);
            db.Performances.Remove(performance);
            db.SaveChanges();
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
