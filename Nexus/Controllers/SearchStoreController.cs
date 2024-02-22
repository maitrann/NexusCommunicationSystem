using Microsoft.AspNetCore.Mvc;
using Nexus.Models.Repository;

namespace Nexus.Controllers
{
    public class SearchStoreController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            ViewBag.lsDistrict = NexusRepository.Instance.GetDistricts();
            var modeloff = NexusRepository.Instance.GetSto();

            return View(modeloff);
        }
        public IActionResult SearchStore(int id)
        {
            var modeloff = NexusRepository.Instance.GetSto();
            if (id == 0)
            {
            return PartialView("_SearchStore", modeloff);

            } else
            {
                var model = NexusRepository.Instance.GetStores(id);
                return PartialView("_SearchStore", model);
            }

        }
    }
}
