using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nexus.Models.ModelView;
using Nexus.Models.Repository;
using System.Text.Json.Nodes;

namespace Nexus.Controllers
{
    public class ServicePlanController : Controller
    {
        public IActionResult Index()
        {
            var model = NexusRepository.Instance.getPaintToShow();
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            ViewBag.lsExpiry = NexusRepository.Instance.GetExpiryDates();
            return View(model);
        }
        public IActionResult ServicePlanDetail(ServicePlanView idSer)
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            var model = NexusRepository.Instance.getPaintToShowbyID(idSer);
            ViewBag.IdCate = model.CategoryID;
            return View(model);
        }
        public IActionResult ServicePlanWithCategory(int id)
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            var model = NexusRepository.Instance.getPaintToShowWithCate(id);
            TempData["idCate"] = id;
            return View(model);
        }

        public IActionResult SearchServicePlan(string SelectPrice, int SelectExpiry)
        {
            var serviceplan = NexusRepository.Instance.getPaintToShow();

            if (SelectPrice == "1")
            {
                if (SelectExpiry != 0)
                {
                    var service = NexusRepository.Instance.SearchExpiry(SelectExpiry).OrderByDescending(x => x.Price).ToList();
                    return PartialView("_ServicePlanPrice", service);
                }
                else
                {
                    serviceplan = serviceplan.OrderByDescending(x => x.Price).ToList();
                    return PartialView("_ServicePlanPrice", serviceplan);
                }
            }
            else if (SelectPrice == "2")
            {
                if (SelectExpiry != 0)
                {
                    var service = NexusRepository.Instance.SearchExpiry(SelectExpiry).OrderBy(x => x.Price).ToList(); ;
                    return PartialView("_ServicePlanPrice", service);
                }
                else
                {
                    serviceplan = serviceplan.OrderBy(x => x.Price).ToList();
                    return PartialView("_ServicePlanPrice", serviceplan);
                }
            }
            else
            {
                if (SelectExpiry != 0)
                {
                    var service = NexusRepository.Instance.SearchExpiry(SelectExpiry);
                    return PartialView("_ServicePlanPrice", service);
                }
                else
                {
                    return PartialView("_ServicePlanPrice", serviceplan);

                }
            }
        }

        public IActionResult SearchServicePlanWithCate(string SelectPrice, int id)
        {
            var serviceplan = NexusRepository.Instance.getPaintToShowWithCate(id);
            if (SelectPrice == "1")
            {
                serviceplan = serviceplan.OrderByDescending(x => x.Price).ToList();
                return PartialView("_ServicePlanPrice", serviceplan);
            }
            else if (SelectPrice == "2")
            {
                serviceplan = serviceplan.OrderBy(x => x.Price).ToList();
                return PartialView("_ServicePlanPrice", serviceplan);
            }
            else
            {
                return PartialView("_ServicePlanPrice", serviceplan);
            }
        }


    }
}
