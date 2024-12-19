using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using W2;

namespace W2.Controllers
{
    public class CatalogiesController : Controller
    {
        private WineStoreDB db = new WineStoreDB();

        // GET: Catalogies
        public ActionResult Index()
        {
            return View(db.Catalogy.ToList());
        }

        // GET: Catalogies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogy catalogy = db.Catalogy.Find(id);
            if (catalogy == null)
            {
                return HttpNotFound();
            }
            return View(catalogy);
        }

        // GET: Catalogies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalogies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CatalogyID,CatalogyName,Description")] Catalogy catalogy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Catalogy.Add(catalogy);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = " Lỗi nhập thông tin" + ex.Message;
                return View(catalogy);
            }
        }

        // GET: Catalogies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogy catalogy = db.Catalogy.Find(id);
            if (catalogy == null)
            {
                return HttpNotFound();
            }
            return View(catalogy);
        }

        // POST: Catalogies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CatalogyID,CatalogyName,Description")] Catalogy catalogy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(catalogy).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi dữ liệu" + ex.Message;

                return View(catalogy);
            }
                
        }

        // GET: Catalogies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogy catalogy = db.Catalogy.Find(id);
            if (catalogy == null)
            {
                return HttpNotFound();
            }
            return View(catalogy);
        }

        // POST: Catalogies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Catalogy catalogy = db.Catalogy.Find(id);
            try
            {
                db.Catalogy.Remove(catalogy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi không thể xoá" +ex.Message;
                return View("Delete", catalogy);
            }
          
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
