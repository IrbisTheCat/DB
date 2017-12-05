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
     [Authorize(Roles ="moderator")]
    public class BandMembersController : Controller
    {
        private Entities db = new Entities();

        // GET: BandMembers
        public ActionResult Index()
        {
            return View(db.BandMembers.ToList());
        }

        // GET: BandMembers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandMember bandMember = db.BandMembers.Find(id);
            if (bandMember == null)
            {
                return HttpNotFound();
            }
            return View(bandMember);
        }

        // GET: BandMembers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BandMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Age,InstrumentPlayed")] BandMember bandMember)
        {
            if (ModelState.IsValid)
            {
                db.BandMembers.Add(bandMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bandMember);
        }

        // GET: BandMembers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandMember bandMember = db.BandMembers.Find(id);
            if (bandMember == null)
            {
                return HttpNotFound();
            }
            return View(bandMember);
        }

        // POST: BandMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Age,InstrumentPlayed")] BandMember bandMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bandMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bandMember);
        }

        // GET: BandMembers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandMember bandMember = db.BandMembers.Find(id);
            if (bandMember == null)
            {
                return HttpNotFound();
            }
            return View(bandMember);
        }

        // POST: BandMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BandMember bandMember = db.BandMembers.Find(id);
            db.BandMembers.Remove(bandMember);
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
