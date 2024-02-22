using Microsoft.AspNetCore.Mvc;
using Nexus.Models.Repository;

namespace Nexus.Controllers
{
    public class LogoutController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
