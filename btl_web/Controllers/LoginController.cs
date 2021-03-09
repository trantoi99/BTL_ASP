using Framework.EF;
using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace btl_web.Controllers
{
    
    public class Account
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool remmember { get; set; }
    }
    public class LoginController : Controller
    {
        private readonly BTL_Library db = new BTL_Library();
       public ActionResult Index()
        {
            var model = new Account();
            if (Request.Cookies["Account"] != null)
            {
                model.username = Request.Cookies["Account"].Values["UserName"];
                model.password = Request.Cookies["Account"].Values["Password"];
                model.remmember = true;
                return View(model);
            }
            else
            {
                model.remmember = false;
                return View(model);
            }
        }


        public ActionResult Abc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUserName(Account model)
        {
            var msg = new JMessage();
            try
            {
                if(model != null)
                {
                    var checklogin = db.Users.Where(x => x.UserName.Equals(model.username) && x.Password.Equals(model.password)).FirstOrDefault();
                    if(checklogin == null)
                    {
                        msg.Title = "Sai tai khoan hoac mat khau!";
                        msg.Error = true;
                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    if(checklogin.Status == false)
                    {
                        msg.Title = "Tai khoan da bi khoa";
                        msg.Error = true;
                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if(model.remmember == true)
                        {
                            HttpCookie cookie = new HttpCookie("Account");
                            cookie.Values.Add("UserName", model.username);
                            cookie.Values.Add("Password", model.password);
                            cookie.Expires = DateTime.Now.AddDays(10);
                            Response.Cookies.Add(cookie);
                        }
                        else
                        {
                            Response.Cookies["Account"].Expires = DateTime.Now.AddDays(-30);
                        }
                        msg.Error = false;
                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }

                }
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                msg.Error = true;
                msg.Title = "Co loi xay ra trong qua trinh thuc hien!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
