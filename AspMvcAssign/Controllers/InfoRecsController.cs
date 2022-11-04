using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspMvcAssign.Models;

namespace AspMvcAssign.Controllers
{
    public class InfoRecsController : Controller
    {
        private Assignment2Entities3 db = new Assignment2Entities3();

        // GET: InfoRecs
        public ActionResult Index()
        {
            return View(db.InfoRecs.ToList());
        }

        // GET: InfoRecs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfoRec infoRec = db.InfoRecs.Find(id);
            if (infoRec == null)
            {
                return HttpNotFound();
            }
            return View(infoRec);
        }

        // GET: InfoRecs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InfoRecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Activity,Time,UserId")] InfoRec infoRec)
        {
            if (ModelState.IsValid)
            {
                db.InfoRecs.Add(infoRec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(infoRec);
        }

        // GET: InfoRecs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfoRec infoRec = db.InfoRecs.Find(id);
            if (infoRec == null)
            {
                return HttpNotFound();
            }
            return View(infoRec);
        }

        // POST: InfoRecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Activity,Time,UserId")] InfoRec infoRec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(infoRec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(infoRec);
        }

        // GET: InfoRecs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfoRec infoRec = db.InfoRecs.Find(id);
            if (infoRec == null)
            {
                return HttpNotFound();
            }
            return View(infoRec);
        }

        // POST: InfoRecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InfoRec infoRec = db.InfoRecs.Find(id);
            db.InfoRecs.Remove(infoRec);
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
