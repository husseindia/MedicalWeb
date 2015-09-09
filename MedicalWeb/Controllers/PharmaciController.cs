using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalWeb.Models;

namespace MedicalWeb.Controllers
{
    public class PharmaciController : Controller
    {
        private MedicalContext db = new MedicalContext();

        //
        // GET: /Pharmaci/

        public ActionResult Index()
        {
            return View(db.Pharmcis.ToList());
        }

        //
        // GET: /Pharmaci/Details/5

        public ActionResult Details(Guid id)
        {
            Pharmci pharmci = db.Pharmcis.Find(id);
            if (pharmci == null)
            {
                return HttpNotFound();
            }
            return View(pharmci);
        }

        //
        // GET: /Pharmaci/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Pharmaci/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pharmci pharmci)
        {
            if (ModelState.IsValid)
            {
                pharmci.Id = Guid.NewGuid();
                db.Pharmcis.Add(pharmci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pharmci);
        }

        //
        // GET: /Pharmaci/Edit/5

        public ActionResult Edit(Guid id)
        {
            Pharmci pharmci = db.Pharmcis.Find(id);
            if (pharmci == null)
            {
                return HttpNotFound();
            }
            return View(pharmci);
        }

        //
        // POST: /Pharmaci/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pharmci pharmci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pharmci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pharmci);
        }

        //
        // GET: /Pharmaci/Delete/5

        public ActionResult Delete(Guid id)
        {
            Pharmci pharmci = db.Pharmcis.Find(id);
            if (pharmci == null)
            {
                return HttpNotFound();
            }
            return View(pharmci);
        }

        //
        // POST: /Pharmaci/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Pharmci pharmci = db.Pharmcis.Find(id);
            db.Pharmcis.Remove(pharmci);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}