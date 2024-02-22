using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.Helper;
using Nexus.Models.Dao;
using Nexus.Models.Model;
using Nexus.Models.ModelView;
using Nexus.Models.Repository;
using System.Runtime.InteropServices;

namespace Nexus.Controllers
{
    [SessionExists]
    [Keyless]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            return View();
        }
        [HttpPost]
        public IActionResult Index(CustomerView cus)
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            CustomerView cusLogin = NexusRepository.Instance.getCustomer(cus);
            if (cusLogin.Id != 0 && HashPassword.GetInstance.CheckPass(cus.Password, cusLogin.Password))
            {
                HttpContext.Session.SetString("Customer", cusLogin.Id.ToString());
                return Redirect("/Storage");
            }
            else
            {
                TempData["AlertError"] = "Error";
                return Redirect("/Login");
            }
        }

        //login k co tkhoan db
        public IActionResult CheckLogin()
        {
            string phone = Request.Form["phone"];
            string pass = Request.Form["password"];
            
            CustomerView cusLog = NexusRepository.Instance.checkLogin(phone, pass);
            //var ck = cusLog.Password;
            if (cusLog.Id != 0 && HashPassword.GetInstance.CheckPass(pass, cusLog.Password))
            {
                HttpContext.Session.SetString("Customer", cusLog.Id.ToString());
                //return RedirectToAction("/Storage");
                return RedirectToAction("Index", "Storage");
            }
            else
            {
                TempData["AlertError"] = "Error";
                return Redirect("/Login");
            }

        }
    }
}
