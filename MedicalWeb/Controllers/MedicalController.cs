using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalWeb.Models;

using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;

namespace MedicalWeb.Controllers
{
    public class MedicalController : Controller
    {
        private MedicalContext db = new MedicalContext();
        private int field;
        private const int Constant = 1;

        //
        // GET: /Medical/

        public ActionResult Index()
        {
            return View(db.Medicals.ToList());
        }

        //
        // GET: /Medical/Details/5

        public ActionResult Details(Guid id)
        {
            Medical medical = db.Medicals.Find(id);
            if (medical == null)
            {
                return HttpNotFound();
            }
            return View(medical);
        }

        //
        // GET: /Medical/Create

        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.MedicalTypes, "id", "Type");
            return View();
        }

        //
        // POST: /Medical/Create
        [System.Web.Mvc.HttpPost]
        //[HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Create(Medical medical)
        {
            if (ModelState.IsValid)
            {
                medical.id = Guid.NewGuid();
                db.Medicals.Add(medical);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medical);
        }

        //
        // GET: /Medical/Edit/5

        public ActionResult Edit(Guid id)
        {
            Medical medical = db.Medicals.Find(id);
            if (medical == null)
            {
                return HttpNotFound();
            }
            return View(medical);
        }

        //
        // POST: /Medical/Edit/5

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult Edit(Medical medical)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medical).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medical);
        }

        //
        // GET: /Medical/Delete/5

        public ActionResult Delete(Guid id)
        {
            Medical medical = db.Medicals.Find(id);
            if (medical == null)
            {
                return HttpNotFound();
            }
            return View(medical);
        }

        //
        // POST: /Medical/Delete/5

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Medical medical = db.Medicals.Find(id);
            db.Medicals.Remove(medical);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult GetMedicalWithType(Guid parentID)
        {
            return Json(db.Medicals.Where(t => t.ParentId == parentID), JsonRequestBehavior.AllowGet);
        }

        public void Method()
        {
            throw new System.NotImplementedException();
        }

        //    public HttpResponseMessage Post()
        //    {
        //        HttpResponseMessage result = null;
        //        var httpRequest = HttpContext.Current.Request;
        //        if (httpRequest.Files.Count > 0)
        //        {
        //            var docfiles = new List<string>();
        //            foreach (string file in httpRequest.Files)
        //            {
        //                var postedFile = httpRequest.Files[file];
        //                var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
        //                postedFile.SaveAs(filePath);

        //                docfiles.Add(filePath);
        //            }
        //            result =System.Net.Http.HttpRequestMessage.CreateResponse(HttpStatusCode.Created, docfiles);
        //        }
        //        else
        //        {
        //            result = System.Net.Http.Formatting.re Request.CreateResponse(HttpStatusCode.BadRequest);
        //        }
        //        return result;
        //    }
        //}
    }
}
