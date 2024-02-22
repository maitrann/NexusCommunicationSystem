using Microsoft.AspNetCore.Mvc;
using Nexus.Models.Repository;

namespace Nexus.Controllers
{
    public class MainPageController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            ViewBag.Service = NexusRepository.Instance.getPaintToShowBestSeller();
            ViewBag.Service2 = NexusRepository.Instance.getPaintToShowSpecial();
            ViewBag.Blogs = NexusRepository.Instance.GetBlogTop4();
            return View();
        }

    }
}
