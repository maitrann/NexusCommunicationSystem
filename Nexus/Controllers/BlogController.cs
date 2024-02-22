using Microsoft.AspNetCore.Mvc;
using Nexus.Models.Repository;

namespace Nexus.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            var model = NexusRepository.Instance.GetBlogs();
            return View(model);
        }
        public IActionResult Detail(int id)
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            var model = NexusRepository.Instance.GetDetailBlog(id);
            if (model !=null)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }
    }
}
