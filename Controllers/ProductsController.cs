using PagedList;
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
    public class ProductsController : Controller
    {
        private WineStoreDB db = new WineStoreDB();

        // GET: Products
        public ActionResult Index(string SortOrder, String searchString, String currentFiller, int? page)
        {

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFiller;
            }
            ViewBag.currentFiller = searchString;

            var product = db.Product.Include(p => p.Catalogy);
           
            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(p => p.ProductName.Contains(searchString));
            }
            ViewBag.SapxepTheoTen = String.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.SapxepTheoGia = SortOrder == "Gia" ? "gia_desc" : "Gia";
            switch (SortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(s => s.ProductName);
                    break;
                case "Gia":
                    product = product.OrderByDescending(s => s.Price);
                    break;
                case "gia_desc":
                    product = product.OrderBy(s => s.Price);
                    break;
                default:
                    product= product.OrderBy(s => s.ProductName);
                    break;

            }
            int pagesize = 10;
            int numberPage = (page ?? 1);
            return View(product.ToPagedList(numberPage, pagesize));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CatalogyID = new SelectList(db.Catalogy, "CatalogyID", "CatalogyName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,Description,PurchasePrice,Price,Quantity,Vintage,CatalogyID,Image,Region")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.Image = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        //Use Namespace called: System.IO
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        //Lấy tên file upload
                        string UploadPath = Server.MapPath("~/wwwroot/WineImages/" + FileName);
                        //Copy và lưu file vào server.
                        f.SaveAs(UploadPath);
                        //Lưu tên file vào trường Image product. Image FileName;
                    product.Image = FileName;
                    }
                    db.Product.Add(product);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error ="Lỗi nhập dữ liệu " + ex.Message;
                ViewBag.CatalogyID = new SelectList(db.Catalogy, "CatalogyID", "CatalogyName", product.CatalogyID);
                return View(product);
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatalogyID = new SelectList(db.Catalogy, "CatalogyID", "CatalogyName", product.CatalogyID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,Description,PurchasePrice,Price,Quantity,Vintage,CatalogyID,Image,Region")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                ViewBag.CatalogyID = new SelectList(db.Catalogy, "CatalogyID", "CatalogyName", product.CatalogyID);
                return View(product);
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            try
            {
                db.Product.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không thể xoá. " + ex.Message;
                return View("Delete", product);
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
