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
    public class MedicalTypeController : Controller
    {
        private MedicalContext db = new MedicalContext();

        //
        // GET: /MedicalType/

        public ActionResult Index()
        {
            return View(db.MedicalTypes.ToList());
        }

        //
        // GET: /MedicalType/Details/5

        public ActionResult Details(Guid id)
        {
            MedicalType medicaltype = db.MedicalTypes.Find(id);
            if (medicaltype == null)
            {
                return HttpNotFound();
            }
            return View(medicaltype);
        }

        //
        // GET: /MedicalType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MedicalType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicalType medicaltype)
        {
            if (ModelState.IsValid)
            {
                medicaltype.id = Guid.NewGuid();
                db.MedicalTypes.Add(medicaltype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medicaltype);
        }

        //
        // GET: /MedicalType/Edit/5

        public ActionResult Edit(Guid id)
        {
            MedicalType medicaltype = db.MedicalTypes.Find(id);
            if (medicaltype == null)
            {
                return HttpNotFound();
            }
            return View(medicaltype);
        }

        //
        // POST: /MedicalType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MedicalType medicaltype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicaltype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicaltype);
        }

        //
        // GET: /MedicalType/Delete/5

        public ActionResult Delete(Guid id)
        {
            MedicalType medicaltype = db.MedicalTypes.Find(id);
            if (medicaltype == null)
            {
                return HttpNotFound();
            }
            return View(medicaltype);
        }

        //
        // POST: /MedicalType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MedicalType medicaltype = db.MedicalTypes.Find(id);
            db.MedicalTypes.Remove(medicaltype);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetAllMedicalType()
        {
            return Json(db.MedicalTypes, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}