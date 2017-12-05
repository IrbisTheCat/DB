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
    public class PerformanceCommentsController : Controller
    {
        private Entities db = new Entities();

        // GET: PerformanceComments
        public ActionResult Index(int perfId)
        {
            
            var performanceCommentThreads = db.PerformanceCommentThreads.Include(p => p.AspNetUser).Include(p => p.Performance).Where(p => p.PerformanceID == perfId).ToList();
            var perf = db.Performances.Where(per => per.Id ==perfId).SingleOrDefault();
            var loc = db.Locations.Where(loct => loct.Id == perf.LocationId).SingleOrDefault();
            //TODO Fix Query tO DISPLAY ALL THE SONGS
            var setList = db.SetLists.Include(z => z.Song).Where(p => p.PerformanceID == perf.Id).ToList();
            ViewBag.Set = setList;
            ViewBag.Loc = loc;
            ViewBag.Perf = perf;
            ViewBag.IDT = perfId;
            return View(performanceCommentThreads.ToList());
        }

        // GET: PerformanceComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceCommentThread performanceCommentThread = db.PerformanceCommentThreads.Find(id);
            if (performanceCommentThread == null)
            {
                return HttpNotFound();
            }
            return View(performanceCommentThread);
        }

        // GET: PerformanceComments/Create
        public ActionResult Create(int? id)
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers.Where(u => u.Email == User.Identity.Name), "Id", "Email");
            var performanceAttended = db.ShowLists.Where(fb => fb.AspNetUser.Email == User.Identity.Name);
            ViewBag.PerformanceID = new SelectList(db.Performances.Join(performanceAttended, p=>p.Id, fb=>fb.PerformanceId, (p,fb)=>p), "Id", "Name");
            return View();
        }

        // POST: PerformanceComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Comment,UserId,PerformanceID")] PerformanceCommentThread performanceCommentThread)
        {
            if (ModelState.IsValid)
            {
                db.PerformanceCommentThreads.Add(performanceCommentThread);
                db.SaveChanges();
                return RedirectToAction("Index", new { perfId = performanceCommentThread.PerformanceID });
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers.Where(u => u.Email == User.Identity.Name), "Id", "Email");
            var performanceAttended = db.ShowLists.Where(fb => fb.AspNetUser.Email == User.Identity.Name);
            ViewBag.PerformanceID = new SelectList(db.Performances.Join(performanceAttended, p => p.Id, fb => fb.PerformanceId, (p, fb) => p), "Id", "Name");
            return View(performanceCommentThread);
        }

        // GET: PerformanceComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceCommentThread performanceCommentThread = db.PerformanceCommentThreads.Find(id);
            if (performanceCommentThread == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers.Where(u => u.Email == User.Identity.Name), "Id", "Email");
            var performanceAttended = db.ShowLists.Where(fb => fb.AspNetUser.Email == User.Identity.Name);
            ViewBag.PerformanceID = new SelectList(db.Performances.Join(performanceAttended, p => p.Id, fb => fb.PerformanceId, (p, fb) => p), "Id", "Name");
            return View(performanceCommentThread);
        }

        // POST: PerformanceComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Comment,UserId,PerformanceID")] PerformanceCommentThread performanceCommentThread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(performanceCommentThread).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers.Where(u => u.Email == User.Identity.Name), "Id", "Email");
            var performanceAttended = db.ShowLists.Where(fb => fb.AspNetUser.Email == User.Identity.Name);
            ViewBag.PerformanceID = new SelectList(db.Performances.Join(performanceAttended, p => p.Id, fb => fb.PerformanceId, (p, fb) => p), "Id", "Name");
            return View(performanceCommentThread);
        }

        // GET: PerformanceComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceCommentThread performanceCommentThread = db.PerformanceCommentThreads.Find(id);
            if (performanceCommentThread == null)
            {
                return HttpNotFound();
            }
            return View(performanceCommentThread);
        }

        // POST: PerformanceComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int perfID)
        {
            PerformanceCommentThread performanceCommentThread = db.PerformanceCommentThreads.Find(id);
            db.PerformanceCommentThreads.Remove(performanceCommentThread);
            db.SaveChanges();
            return RedirectToAction("Index", new { perfId = performanceCommentThread.PerformanceID });
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
