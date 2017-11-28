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
    public class SetListsController : Controller
    {
        private Entities db = new Entities();

        // GET: SetLists
        public ActionResult Index()
        {
            var setLists = db.SetLists.Include(s => s.Performance).Include(s => s.Song);
            return View(setLists.ToList());
        }

        // GET: SetLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetList setList = db.SetLists.Find(id);
            if (setList == null)
            {
                return HttpNotFound();
            }
            return View(setList);
        }

        // GET: SetLists/Create
        public ActionResult Create()
        {
            ViewBag.PerformanceID = new SelectList(db.Performances, "Id", "Name");
            ViewBag.SongID = new SelectList(db.Songs, "Id", "SongName");
            return View();
        }

        // POST: SetLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PerformanceID,SongID")] SetList setList)
        {
            if (ModelState.IsValid)
            {
                db.SetLists.Add(setList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PerformanceID = new SelectList(db.Performances, "Id", "Name", setList.PerformanceID);
            ViewBag.SongID = new SelectList(db.Songs, "Id", "SongName", setList.SongID);
            return View(setList);
        }

        // GET: SetLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetList setList = db.SetLists.Find(id);
            if (setList == null)
            {
                return HttpNotFound();
            }
            ViewBag.PerformanceID = new SelectList(db.Performances, "Id", "Name", setList.PerformanceID);
            ViewBag.SongID = new SelectList(db.Songs, "Id", "SongName", setList.SongID);
            return View(setList);
        }

        // POST: SetLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PerformanceID,SongID")] SetList setList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PerformanceID = new SelectList(db.Performances, "Id", "Name", setList.PerformanceID);
            ViewBag.SongID = new SelectList(db.Songs, "Id", "SongName", setList.SongID);
            return View(setList);
        }

        // GET: SetLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetList setList = db.SetLists.Find(id);
            if (setList == null)
            {
                return HttpNotFound();
            }
            return View(setList);
        }

        // POST: SetLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SetList setList = db.SetLists.Find(id);
            db.SetLists.Remove(setList);
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
