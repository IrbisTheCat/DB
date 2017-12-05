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
    [Authorize]
    public class FavoriteBandCommentsController : Controller
    {
        private Entities db = new Entities();

        // GET: FavoriteBandComments
        public ActionResult Index(int id)
        {
            var favoriteBands = db.FavoriteBandLists.Where(fb => fb.AspNetUser.Email == User.Identity.Name);
            var bandComments = db.BandComments.Join(favoriteBands, bc=>bc.BandID, fb=>fb.BandId, (bc,fb)=> bc).Include(b => b.AspNetUser).Include(b => b.Band);
            return View(bandComments.Where(bc=> bc.BandID==id).ToList());
        }

        // GET: FavoriteBandComments/Details/5
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

        // GET: FavoriteBandComments/Create
        /*@
         * 
         */
        public ActionResult Create(int? id)
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers.Where(u=>u.Email== User.Identity.Name), "Id", "Email");
            var favoriteBands = db.FavoriteBandLists.Where( fb=>fb.AspNetUser.Email== User.Identity.Name);
            ViewBag.BandID = new SelectList(db.Bands.Join(favoriteBands, b=>b.Id, fb=>fb.BandId, (b,fb) => b), "Id", "BandName");
            return View();
        }

        // POST: FavoriteBandComments/Create
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
                return RedirectToAction("Index", new { id=bandComment.BandID});
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers.Where(u => u.Email == User.Identity.Name), "Id", "Email", bandComment.UserId);
            var favoriteBands = db.FavoriteBandLists.Where(fb => fb.AspNetUser.Email == User.Identity.Name);
            ViewBag.BandID = new SelectList(db.Bands.Join(favoriteBands, b => b.Id, fb => fb.BandId, (b, fb) => b), "Id", "BandName", bandComment.BandID);
            return View(bandComment);
        }

        // GET: FavoriteBandComments/Edit/5
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
            ViewBag.UserId = new SelectList(db.AspNetUsers.Where(u => u.Email == User.Identity.Name), "Id", "Email", bandComment.UserId);
            var favoriteBands = db.FavoriteBandLists.Where(fb => fb.AspNetUser.Email == User.Identity.Name);
            ViewBag.BandID = new SelectList(db.Bands.Join(favoriteBands, b => b.Id, fb => fb.BandId, (b, fb) => b), "Id", "BandName", bandComment.BandID);
            return View(bandComment);
        }

        // POST: FavoriteBandComments/Edit/5
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
                return RedirectToAction("Index", new { id = bandComment.BandID });
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers.Where(u => u.Email == User.Identity.Name), "Id", "Email", bandComment.UserId);
            var favoriteBands = db.FavoriteBandLists.Where(fb => fb.AspNetUser.Email == User.Identity.Name);
            ViewBag.BandID = new SelectList(db.Bands.Join(favoriteBands, b => b.Id, fb => fb.BandId, (b, fb) => b), "Id", "BandName", bandComment.BandID);
            return View(bandComment);
        }

        // GET: FavoriteBandComments/Delete/5
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

        // POST: FavoriteBandComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BandComment bandComment = db.BandComments.Find(id);
            db.BandComments.Remove(bandComment);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = bandComment.BandID });
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
