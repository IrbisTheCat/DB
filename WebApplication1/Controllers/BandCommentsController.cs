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
    public class BandCommentsController : Controller
    {
        private Entities db = new Entities();

        // GET: BandComments
        public ActionResult Index(int bandId)
        {
            Session["bandId"] = bandId;
            var bandComments = db.BandComments.Include(b => b.AspNetUser).Include(b => b.Band).Where(b=>b.BandID == bandId);
            return View(bandComments.ToList());
        }

        // GET: BandComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandComment bandComment = db.BandComments.Find(id);
            if (bandComment == null)
            {
                return HttpNotFound();
            }
            return View(bandComment);
        }

        // GET: BandComments/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.BandID = new SelectList(db.Bands, "Id", "BandName");
            return View();
        }

        // POST: BandComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BandID,UserId,Comment")] BandComment bandComment)
        {
            if (ModelState.IsValid)
            {
                db.BandComments.Add(bandComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", bandComment.UserId);
            ViewBag.BandID = new SelectList(db.Bands, "Id", "BandName", bandComment.BandID);
            return View(bandComment);
        }

        [Authorize]
        // GET: BandComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandComment bandComment = db.BandComments.Find(id);
            if (bandComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", bandComment.UserId);
            ViewBag.BandID = new SelectList(db.Bands, "Id", "BandName", bandComment.BandID);
            return View(bandComment);
        }

        // POST: BandComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BandID,UserId,Comment")] BandComment bandComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bandComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", bandComment.UserId);
            ViewBag.BandID = new SelectList(db.Bands, "Id", "BandName", bandComment.BandID);
            return View(bandComment);
        }

        // GET: BandComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandComment bandComment = db.BandComments.Find(id);
            if (bandComment == null)
            {
                return HttpNotFound();
            }
            return View(bandComment);
        }

        // POST: BandComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BandComment bandComment = db.BandComments.Find(id);
            db.BandComments.Remove(bandComment);
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
