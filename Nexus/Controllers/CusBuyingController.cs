using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Nexus.Models;
using Nexus.Models.Model;
using Nexus.Models.ModelView;
using Nexus.Models.Repository;
using Razorpay.Api;
using System.Collections.Generic;

namespace Nexus.Controllers
{
    [SessionCheck]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class CusBuyingController : Controller
    {
        public IActionResult Information(int ServicePlanID, int quantity, int expiry)
        {
            ViewBag.lsCategory = NexusRepository.Instance.getCatetory();
            var model = NexusRepository.Instance.getExpiryDate(ServicePlanID);
            if (!TempData.ContainsKey("quantity"))
            {
                quantity = 1;
            }
            else if (!TempData.ContainsKey("expiry"))
            {
                expiry = 1;
            }
            else
            {
                quantity = Convert.ToInt32(TempData["quantity"]);
                expiry = Convert.ToInt32(TempData["expiry"]);
            }
            ServicePlanView servicePlanView = (ServicePlanView)NexusRepository.Instance.GetServiceByID(ServicePlanID);
            ViewBag.ServicePlan = servicePlanView;
            ViewBag.service = NexusRepository.Instance.getServicePlan(ServicePlanID, quantity, expiry);

            //int CustomerID = 1;
            //CusBuyingView modelInsert = new CusBuyingView( ServicePlanID =  , idcus = CustomerID );
            //bool check = NexusRepository.Instance.insertCusbuying( modelInsert );
            //if (check)
            //{
            //    TempData["Alert"] = "Success";
            //    return Redirect("/AllService");
            //}
            //else
            //{
            //    TempData["Alert"] = "Error";
            //    return Redirect("/AllService");
            //}
            return View(model);
        }
        public IActionResult Checkout(int id, int quantity, int expiry)
        {
            quantity = Convert.ToInt32(Request.Form["quantity"]);
            expiry = Convert.ToInt32(Request.Form["selectTime"]);

            TempData["quantity"] = quantity;
            TempData["expiry"] = expiry;
            return RedirectToAction("Information", new { ServicePlanID = id});
        }

        [HttpPost]
        public ActionResult CreateOrder(PaymentRequest _requestData)
        {
            // Generate random receipt number for order
            Random randomObj = new();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();
            RazorpayClient client = new("rzp_test_unAtgRERC0DP5v", "KGPEzBN6fuQfVvaMkh8oJVwh");
            Dictionary<string, object> options = new();
            options.Add("amount", _requestData.Amount * 100);  // Amount will in paise
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "0"); // 1 - automatic  , 0 - manual
            //options.Add("notes", "-- You can put any notes here --");
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();



            // Create order model for return on view
            RazorPayOrder orderModel = new RazorPayOrder
            {
                OrderId = orderResponse.Attributes["id"],
                RazorPayAPIKey = "rzp_test_unAtgRERC0DP5v",
                Amount = _requestData.Amount * 100,
                Currency = "INR",
                Name = _requestData.Name,
                Email = _requestData.Email,
                Phone = _requestData.Phone,
                Address = _requestData.Address,
                Quantity = _requestData.Quantity,
                Total = _requestData.Total,
                ServicePlanId = _requestData.ServicePlanId,
                ExpiryDateId = _requestData.ExpiryDateId,
            };
            // Return on PaymentPage with Order data
            return View("CreateOrder", orderModel);
        }

        [HttpPost]
        public ActionResult Complete()
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("Customer"));

            // Payment data comes in url so we have to get it from url
            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
            string paymentId = HttpContext.Request.Form["rzp_paymentid"].ToString();
            // This is orderId
            string orderId = HttpContext.Request.Form["rzp_orderid"].ToString();
            RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_unAtgRERC0DP5v", "KGPEzBN6fuQfVvaMkh8oJVwh");
            Payment payment = client.Payment.Fetch(paymentId);
            // This code is for capture the payment 
            Dictionary<string, object> options = new();
            options.Add("amount", payment.Attributes["amount"]);
            Payment paymentCaptured = payment.Capture(options);
            int cr = paymentCaptured.Attributes["created_at"];

            DateTime order_date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(cr).ToLocalTime();
            int amt = paymentCaptured.Attributes["amount"];
            int ServicePlanId = int.Parse(Request.Form["ServicePlanId"][0].ToString());
            int ExpiryDateId = int.Parse(Request.Form["ExpiryDateId"][0].ToString());
            int Total = int.Parse(Request.Form["Total"][0].ToString());
            int Quantity = int.Parse(Request.Form["Quantity"][0].ToString());
            string Name = Request.Form["Name"][0].ToString();
            string Phone = Request.Form["Phone"][0].ToString();
            string Address = Request.Form["Address"][0].ToString();
            //// Check payment made successfully
            if (paymentCaptured.Attributes["status"] == "captured")
            {

                // Create these action method
                Models.Order order = NexusRepository.Instance.InsertOrder(ServicePlanId, id, 1, ExpiryDateId, order_date, Name, Phone, Address,
                                            Quantity, amt/100, Total, true, 0);
                NexusRepository.Instance.InsertBill(paymentId, order.Id.ToString(), order_date);
                ViewBag.Message = "Paid successfully";
                ViewBag.Order = paymentCaptured.Attributes;
                return RedirectToAction("Index","MainPage");
            }
            else
            {
                ViewBag.Message = "Payment failed, something went wrong";
                return View("Complete");
            }
        }
    }
}
