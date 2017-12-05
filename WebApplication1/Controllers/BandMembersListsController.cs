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
    public class BandMembersListsController : Controller
    {
        private Entities db = new Entities();

        // GET: BandMembersLists
        public ActionResult Index()
        {
            var bandMembersLists = db.BandMembersLists.Include(b => b.Band).Include(b => b.BandMember1);
            return View(bandMembersLists.ToList());
        }

        // GET: BandMembersLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandMembersList bandMembersList = db.BandMembersLists.Find(id);
            if (bandMembersList == null)
            {
                return HttpNotFound();
            }
            return View(bandMembersList);
        }

        // GET: BandMembersLists/Create
        public ActionResult Create()
        {
            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName");
            ViewBag.BandMember = new SelectList(db.BandMembers, "Id", "FirstName");
            return View();
        }

        // POST: BandMembersLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BandId,BandMember")] BandMembersList bandMembersList)
        {
            if (ModelState.IsValid)
            {
                db.BandMembersLists.Add(bandMembersList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName", bandMembersList.BandId);
            ViewBag.BandMember = new SelectList(db.BandMembers, "Id", "FirstName", bandMembersList.BandMember);
            return View(bandMembersList);
        }

        // GET: BandMembersLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandMembersList bandMembersList = db.BandMembersLists.Find(id);
            if (bandMembersList == null)
            {
                return HttpNotFound();
            }
            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName", bandMembersList.BandId);
            ViewBag.BandMember = new SelectList(db.BandMembers, "Id", "FirstName", bandMembersList.BandMember);
            return View(bandMembersList);
        }

        // POST: BandMembersLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BandId,BandMember")] BandMembersList bandMembersList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bandMembersList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName", bandMembersList.BandId);
            ViewBag.BandMember = new SelectList(db.BandMembers, "Id", "FirstName", bandMembersList.BandMember);
            return View(bandMembersList);
        }

        // GET: BandMembersLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandMembersList bandMembersList = db.BandMembersLists.Find(id);
            if (bandMembersList == null)
            {
                return HttpNotFound();
            }
            return View(bandMembersList);
        }

        // POST: BandMembersLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BandMembersList bandMembersList = db.BandMembersLists.Find(id);
            db.BandMembersLists.Remove(bandMembersList);
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
