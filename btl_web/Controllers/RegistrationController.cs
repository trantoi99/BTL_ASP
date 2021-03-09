using Framework.EF;
using System.Linq;
using System.Web.Mvc;

namespace btl_web.Controllers
{
    public class RegistrationController : Controller
    {
        
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            User account = new User();

            return View(account);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User account)
        {
            using (BTL_Library dataprovider = new BTL_Library()) 
            {
                if (dataprovider.Users.Any(x=>x.UserName==account.UserName))
                {
                    ModelState.AddModelError("","Tài Khoản đã tồn tại");
                }
                else if(ModelState.IsValid)
                {
                    dataprovider.Users.Add(account);
                    dataprovider.SaveChanges();
                    ModelState.AddModelError("", account.UserName + " " + "Đăng ký thành công");
                }
            };

            return View();
        }
    }
}
