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
    public class ShowListController : Controller
    {
        private Entities db = new Entities();

        // GET: ShowList
        public ActionResult Index()
        {
            var shows = db.ShowLists.Where(u => u.AspNetUser.Email == User.Identity.Name);
           // var showLists = db.ShowLists.Include(s => s.AspNetUser).Include(s => s.Performance);
            return View(shows.ToList());
        }

        // GET: ShowList/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowList showList = db.ShowLists.Find(id);
            if (showList == null)
            {
                return HttpNotFound();
            }
            return View(showList);
        }

        // GET: ShowList/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers.Where(x => x.Email == User.Identity.Name), "Id", "Email");
            ViewBag.PerformanceId = new SelectList(db.Performances, "Id", "Name");
            return View();
        }

        // POST: ShowList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,PerformanceId")] ShowList showList)
        {
            if (ModelState.IsValid)
            {
                db.ShowLists.Add(showList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers.Where(x => x.Email == User.Identity.Name), "Id", "Email", showList.UserId);
            ViewBag.PerformanceId = new SelectList(db.Performances, "Id", "Name", showList.PerformanceId);
            return View(showList);
        }

        // GET: ShowList/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowList showList = db.ShowLists.Find(id);
            if (showList == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers.Where(x => x.Email == User.Identity.Name), "Id", "Email", showList.UserId);
            ViewBag.PerformanceId = new SelectList(db.Performances, "Id", "Name", showList.PerformanceId);
            return View(showList);
        }

        // POST: ShowList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,PerformanceId")] ShowList showList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(showList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers.Where(x => x.Email == User.Identity.Name), "Id", "Email", showList.UserId);
            ViewBag.PerformanceId = new SelectList(db.Performances, "Id", "Name", showList.PerformanceId);
            return View(showList);
        }

        // GET: ShowList/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowList showList = db.ShowLists.Find(id);
            if (showList == null)
            {
                return HttpNotFound();
            }
            return View(showList);
        }

        // POST: ShowList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShowList showList = db.ShowLists.Find(id);
            db.ShowLists.Remove(showList);
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
