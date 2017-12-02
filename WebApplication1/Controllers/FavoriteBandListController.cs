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
    public class FavoriteBandListController : Controller
    {
        private Entities db = new Entities();

        // GET: FavoriteBandList
        public ActionResult Index()
        {
           
            var favoriteBands = db.FavoriteBandLists.Where(fb => fb.AspNetUser.Email == User.Identity.Name);
            return View(favoriteBands.ToList());
        }

  

        // GET: FavoriteBandList/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers.Where( x=> x.Email == User.Identity.Name), "Id", "Email");
            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName");
            return View();
        }

        // POST: FavoriteBandList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,BandId")] FavoriteBandList favoriteBandList)
        {
            if (ModelState.IsValid)
            {
                db.FavoriteBandLists.Add(favoriteBandList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.UserId = new SelectList(db.AspNetUsers.Where(x => x.Email == User.Identity.Name), "Id", "Email", favoriteBandList.UserId);
            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName", favoriteBandList.BandId);

            return View(favoriteBandList);
        }



        // GET: FavoriteBandList/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteBandList favoriteBandList = db.FavoriteBandLists.Find(id);
            if (favoriteBandList == null)
            {
                return HttpNotFound();
            }
            return View(favoriteBandList);
        }

        // POST: FavoriteBandList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FavoriteBandList favoriteBandList = db.FavoriteBandLists.Find(id);
            db.FavoriteBandLists.Remove(favoriteBandList);
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
