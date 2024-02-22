using Microsoft.AspNetCore.Mvc;
using Nexus.Helper;
using Nexus.Models.Model;
using Nexus.Models.ModelView;
using Nexus.Models.Repository;

namespace Nexus.Controllers
{
    [SessionExists]
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            return View();
        }

        [HttpPost]
        public IActionResult Index(CustomerView cv)
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            cv.Password = HashPassword.GetInstance.HashPass(cv.Password);
            if (ModelState.IsValid)
            {
                bool checkPhone = NexusRepository.Instance.checkExistsPhone(cv.Phone ?? "Null");
                if (!checkPhone)
                {
                    TempData["AlertSuccess"] = "Success";
                    NexusRepository.Instance.customerRegister(cv);
                    return Redirect("/Register");
                }
                else
                {
                    TempData["AlertError"] = "This Phone Already Exists Please Use Different Phone";
                    return View(cv);

                }

            }
            return View(cv);


        }
    }
}
