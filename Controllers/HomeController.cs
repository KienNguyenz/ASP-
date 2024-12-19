using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using W2.Models;

namespace W2.Controllers
{
    public class HomeController : Controller
    {
        WineStoreDB db = new WineStoreDB();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = " Lỗi nhập thông tin hoặc tài khoản đã tồn tại." + ex.Message;
                return View(user);
            }
        }
        public ActionResult Dangnhap()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(User user)
        {

            var taikhoanad = user.TaiKhoan;
            var matkhauad = user.MatKhau;
            var userCheck = db.Users.SingleOrDefault(x => x.TaiKhoan.Equals(taikhoanad) && x.MatKhau.Equals(matkhauad));
            if ( userCheck !=null)
            {
                Session["User"] = userCheck;
                return RedirectToAction("Index", "Catalogies");
            }
            else
            {
                ViewBag.LoginFail = " Đăng nhập thất bại, mời bạn xem lại tài khoản mật khẩu.";
                return View("Dangnhap");
            }
        }

        public ActionResult Dangxuat(User user)
        {
            Session["User"] = null;
            return RedirectToAction("Dangnhap", "Home");
        }
    }
}