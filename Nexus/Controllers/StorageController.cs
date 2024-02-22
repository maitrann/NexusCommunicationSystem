using Microsoft.AspNetCore.Mvc;
using Nexus.Models.Model;
using Nexus.Models.Repository;
using System.ComponentModel;

namespace Nexus.Controllers
{
    [SessionCheck]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class StorageController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            var id = Convert.ToInt32(HttpContext.Session.GetString("Customer"));

            ViewBag.Storage = NexusRepository.Instance.GetOrders(id);
            ViewBag.ProExchange = NexusRepository.Instance.GetProChange();
            //
            ViewBag.OrderExpired = NexusRepository.Instance.GetOrderExpired(id);
            ViewBag.OrderUsing = NexusRepository.Instance.getOrderUsing(id);
            ViewBag.OrderCancel = NexusRepository.Instance.getOrderCancel(id);
            ViewBag.OrderRequest = NexusRepository.Instance.getOrderRequest(id);
            ViewBag.allBill = NexusRepository.Instance.GetOrders(id);

            var model = NexusRepository.Instance.GetCus(id);
            return View(model);
        }
        public IActionResult OrderExpired()
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("Customer"));

            ViewBag.OrderExpired = NexusRepository.Instance.GetOrderExpired(id);
            return PartialView("_OrderExpired");
        }
        public IActionResult OrderUsing()
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("Customer"));
            ViewBag.ProExchange = NexusRepository.Instance.GetProChange();

            ViewBag.OrderUsing = NexusRepository.Instance.getOrderUsing(id);
            return PartialView("_OrderUsing");
        }

        public IActionResult OrderCancel()
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("Customer"));

            ViewBag.OrderCancel = NexusRepository.Instance.getOrderCancel(id);
            return PartialView("_OrderCancelled");
        }
        public IActionResult OrderRequestExchange()
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("Customer"));
            ViewBag.OrderRequest = NexusRepository.Instance.getOrderRequest(id);
            return PartialView("_OrderRequest");
        }
        public IActionResult AllBill()
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("Customer"));
            ViewBag.allBill = NexusRepository.Instance.GetOrders(id);
            return PartialView("_AllBill");
        }


        public IActionResult ChangeService(int idBill)
        {
            int quantityExchange = Convert.ToInt32(Request.Form["quantityChange"]);
            string? reason = Request.Form["reason"];
            NexusRepository.Instance.InsertChange(idBill, quantityExchange, reason);
            return RedirectToAction("Index");
        }

        public IActionResult CancelSP(int id)
        {
            NexusRepository.Instance.cancelSP(id);
            return RedirectToAction("Index");
        }



        public IActionResult ChangeProfile(string name, string email, string phone)
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("Customer"));
            NexusRepository.Instance.changeProfile(id, name, email, phone);
            return RedirectToAction("Index");
        }


        //0 la khong tim thay id user tu sesssion
        //-1 la loi data
        //1 la NewPass khac voi OldPass
        //2 la thanh cong change pass
        public int ChangePass(string oldPass, string newPass)
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("Customer"));
            if (id == null)
            {
                return 0; // Not Found ID
            }
            else
            {
                int idCheck = NexusRepository.Instance.changePass(id, newPass, oldPass);
                return idCheck;
            }

        }




    }
}
