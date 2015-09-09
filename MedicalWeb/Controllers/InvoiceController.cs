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
    public class InvoiceController : Controller
    {
        private MedicalContext db = new MedicalContext();
        private PharmciRepositories Pharmcidb = new PharmciRepositories();
        //
        // GET: /Invoice/

        public ActionResult Index()
        {
            return View(db.InvoiceClasses.ToList());
        }

        //
        // GET: /Invoice/Details/5

        public ActionResult Details(Guid id )
        {
            InvoiceClass invoiceclass = db.InvoiceClasses.Find(id);
            if (invoiceclass == null)
            {
                return HttpNotFound();
            }
            return View(invoiceclass);
        }

        //
        // GET: /Invoice/Create

        public ActionResult Create()
        {

            return View();
        }

        //
        // POST: /Invoice/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceClass invoiceclass)
        {
            if (ModelState.IsValid)
            {
                invoiceclass.id = Guid.NewGuid();
                db.InvoiceClasses.Add(invoiceclass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoiceclass);
        }

        //
        // GET: /Invoice/Edit/5

        public ActionResult Edit(Guid id)
        {
            InvoiceClass invoiceclass = db.InvoiceClasses.Find(id);
            if (invoiceclass == null)
            {
                return HttpNotFound();
            }
            return View(invoiceclass);
        }

        //
        // POST: /Invoice/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InvoiceClass invoiceclass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceclass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoiceclass);
        }

        //
        // GET: /Invoice/Delete/5

        public ActionResult Delete(Guid id)
        {
            InvoiceClass invoiceclass = db.InvoiceClasses.Find(id);
            if (invoiceclass == null)
            {
                return HttpNotFound();
            }
            return View(invoiceclass);
        }

        //
        // POST: /Invoice/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            InvoiceClass invoiceclass = db.InvoiceClasses.Find(id);
            db.InvoiceClasses.Remove(invoiceclass);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);     
        }

        public JsonResult Invoice(string pharmName, Guid medicalId, int quantiti)
        {
            var pharmaci = Pharmcidb.GetByUserName(pharmName);
            try
            {
                if (pharmaci != null)
                {
                    var medical = db.Medicals.Find(medicalId);
                    if (medical != null && medical.quantiti > 0)
                    {
                        db.InvoiceClasses.Add(new InvoiceClass { id = Guid.NewGuid(), pharmName = pharmName, medicalName = medical.name, quantiti = quantiti, price = medical.price });
                        //db.SaveChanges();

                        medical.quantiti = medical.quantiti - quantiti;
                        db.Entry(medical).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                }


                return Json("UnSuccess", JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                return Json("UnSuccess", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetAllMedicalType(string pharmName)
        {
            return Json(db.InvoiceClasses.Where(t => t.pharmName == pharmName), JsonRequestBehavior.AllowGet);
        }
    }
}
