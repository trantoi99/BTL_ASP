using Framework.EF;
using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace btl_web.Controllers
{
    public class HomeAdminController : Controller
    {
        private readonly BTL_Library db = new BTL_Library();
        // GET: HomeAdmin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profile()
        {
            var userID = Convert.ToInt16(Session["UserName"].ToString());
            var profile = db.Users.FirstOrDefault(x => x.Id == userID);
            return View(profile);
        }
        [HttpPost]
        public ActionResult UpdateProfile(User model)
        {
            var updateuser = db.Users.First(x => x.Email == model.Email);
            if (updateuser != null)
            {
                updateuser.FullName = model.FullName;
                updateuser.Email = model.Email;
                updateuser.Address = model.Address;
                db.SaveChanges();
                return Json(new JMessage() { Error = false, Title = "Cập nhật thành công!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new JMessage() { Error = true, Title = "Không tìm thấy người dùng!" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Login");
        }
        [ChildActionOnly]
        public ActionResult _LeftMenu()
        {
            return PartialView();
        }
    }
}