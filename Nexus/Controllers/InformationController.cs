using Microsoft.AspNetCore.Mvc;
using Nexus.Models.Repository;

namespace Nexus.Controllers
{
    public class InformationController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            return View();
        }
    }
}
