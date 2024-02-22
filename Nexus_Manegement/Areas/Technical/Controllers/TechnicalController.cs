using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Nexus_Management.Areas.Accountant.Dao;
using Nexus_Management.Areas.Technical.ModelsView;

using Nexus_Manegement.Areas.Technical.Dao;
using Nexus_Manegement.Models;

namespace Nexus_Management.Areas.Technical.Controllers
{
    [Area("Technical")]
    public class TechnicalController : Controller
    {
        //
        public IActionResult Index()
        {
            if ((HttpContext.Session.GetString("technicallogin")) == "technicallogin")
            {
                int ide = ((int)HttpContext.Session.GetInt32("ide"));
                var model = AccountantDao.Instance.getEmp(ide);
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Management");
            }
        }
        public IActionResult ChangeProfile(string name, string username, string email, string phone)
        {
            if ((HttpContext.Session.GetString("technicallogin")) == "technicallogin")
            {
                int ide = ((int)HttpContext.Session.GetInt32("ide"));
                AccountantDao.Instance.changeProfile(ide, name, username, email, phone);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Management");
            }
        }

        public int ChangePass(string oldPass, string newPass)
        {
            //if ((HttpContext.Session.GetString("sessionlogin")) == "managementlogin")
            //{
            //    int ide = ((int)HttpContext.Session.GetInt32("ide"));
            //    AccountantDao.Instance.changePassword(ide, newPass);
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    return Redirect("Management/Login");
            //}
            int ide = ((int)HttpContext.Session.GetInt32("ide"));

            if (ide == null)
            {
                return 0; // Not Found ID
            }
            else
            {
                int idCheck = TechnicalDao.Instance.changePassword(ide, newPass, oldPass);
                return idCheck;
            }
        }
        public IActionResult Order()
        {
            if ((HttpContext.Session.GetString("technicallogin")) == "technicallogin")
            {

                TechnicalDao.Instance.CountOrderExpiry();

                ViewBag.Year = TechnicalDao.Instance.GetYear();
                TechnicalDao.Instance.CheckConnect();
                var model = TechnicalDao.Instance.getOrder();


                return View(model);
                //return View(list);
            }
            else
            {
                return RedirectToAction("Login", "Management");

            }
        }

        public IActionResult ShowOrderSelect(string year, string month)
        {
            if ((HttpContext.Session.GetString("technicallogin")) == "technicallogin")
            {
                var order = TechnicalDao.Instance.getOrder();
                var orderSelect = TechnicalDao.Instance.getOrderWithSelect();

                if (!string.IsNullOrEmpty(year) && year != DateTime.Now.Year.ToString())
                {
                    if (!string.IsNullOrEmpty(month) && month != "all")
                    {
                        orderSelect = orderSelect.Where(x => x.OrderYear == year && x.OrderDate == month).ToList();
                        return PartialView("_OrderTechnical", orderSelect);
                    }
                    else
                    {
                        orderSelect = orderSelect.Where(x => x.OrderYear == year).ToList();
                        return PartialView("_OrderTechnical", orderSelect);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(month) && month != "all")
                    {
                        order = order.Where(x => x.OrderDate == month).ToList();
                        if (order.Count == 0)
                        {
                            TempData["Message"] = "No result for this month.";
                            return PartialView("_OrderTechnical", order);
                        }
                        else
                        {
                            return PartialView("_OrderTechnical", order);
                        }
                    }
                    else
                    {
                        return PartialView("_OrderTechnical", order);
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Management");

            }
        }
        public int CountOrderExpiry()
        {
            return TechnicalDao.Instance.CountOrderExpiry();
        }
        public IActionResult _MessengerOrderExpired()
        {
            if ((HttpContext.Session.GetString("technicallogin")) == "technicallogin")
            {
                ViewBag.model = TechnicalDao.Instance.MessOrderExpiry();
                return PartialView();
            }
            else
            {
                return RedirectToAction("Login", "Management");

            }
        }
        public IActionResult AboutToExpire()
        {
            if ((HttpContext.Session.GetString("technicallogin")) == "technicallogin")
            {
                var model = TechnicalDao.Instance.MessOrderExpiry();

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Management");
            }
        }
        public IActionResult SendMailAboutToExpire(string mail, int timeSpan, string service)
        {
            if ((HttpContext.Session.GetString("technicallogin")) == "technicallogin")
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("Nexus Service Marketing System", "ntviet1333@gmail.com"));
                email.To.Add(new MailboxAddress("Receiver Name", mail));

                email.Subject = "About To Expire";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = "<b>Your " + service + " has " + timeSpan + " more days to expire. Please visit the homepage for further extension.</b>"
                };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate("ntviet1333@gmail.com", "ufgqbsdlmzofvgcs");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                return RedirectToAction("AjSendMailActionReturn");
            }
            else
            {
                return RedirectToAction("Login", "Management");

            }
        }

        public IActionResult AjSendMailActionReturn()
        {
            if ((HttpContext.Session.GetString("technicallogin")) == "technicallogin")
            {
                ViewBag.lsData = TechnicalDao.Instance.MessOrderExpiry();
                return PartialView("PartAboutToExpire");
            }
            else
            {
                return RedirectToAction("Login", "Management");
            }
        }

        public IActionResult SendMailNewConnect(string mail, string service, string expiredDate)
        {
            if ((HttpContext.Session.GetString("technicallogin")) == "technicallogin")
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("Nexus Service Marketing System", "ntviet1333@gmail.com"));
                email.To.Add(new MailboxAddress("Receiver Name", mail));

                email.Subject = "About To Expire";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = "<b>Your " + service + " has been confirmed. You can start using the " + service + " from " + DateTime.Now.ToString("MM/dd/yyyy") + " until the end of " + expiredDate + ".</ b > "
                };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate("ntviet1333@gmail.com", "ufgqbsdlmzofvgcs");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                return RedirectToAction("AjSendMailActionReturn");
            }
            else
            {
                return RedirectToAction("Login", "Management");

            }
        }
        public IActionResult NewConnect(int id, int idOrder, int usagepack,string mail,string service)
        {
            if ((HttpContext.Session.GetString("technicallogin")) == "technicallogin")
            {
                int ide = ((int)HttpContext.Session.GetInt32("ide"));
                var model = AccountantDao.Instance.getEmp(ide);
                DateTime dateConnect = DateTime.Now;

                if (model.Status == true)
                {
                    TechnicalDao.Instance.NewConnect(id, idOrder, usagepack);
                    switch (usagepack)
                    {
                        case 1:
                            DateTime dateExpiry1 = dateConnect.AddMonths(1);
                            SendMailNewConnect(mail, service, dateExpiry1.ToString("MM/dd/yyyy"));
                            break;
                        case 2:
                            DateTime dateExpiry2 = dateConnect.AddMonths(3);
                            SendMailNewConnect(mail, service, dateExpiry2.ToString("MM/dd/yyyy"));

                            break;
                        case 3:
                            DateTime dateExpiry3 = dateConnect.AddMonths(6);
                            SendMailNewConnect(mail, service, dateExpiry3.ToString("MM/dd/yyyy"));

                            break;
                        case 4:
                            DateTime dateExpiry4 = dateConnect.AddMonths(12);
                            SendMailNewConnect(mail, service, dateExpiry4.ToString("MM/dd/yyyy"));

                            break;
                    }
                    return RedirectToAction("Order");
                }
                else
                {
                    return RedirectToAction("Login", "Management");
                }
            }
            else
            {
                return RedirectToAction("Login", "Management");
            }
        }
        public IActionResult Exchange()
        {
            if ((HttpContext.Session.GetString("technicallogin")) == "technicallogin")
            {
                var model = TechnicalDao.Instance.Exchange();
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Management");

            }

        }
        public IActionResult SaveExchange(int id)
        {
            if ((HttpContext.Session.GetString("technicallogin")) == "technicallogin")
            {
                int ide = ((int)HttpContext.Session.GetInt32("ide"));
                var model = AccountantDao.Instance.getEmp(ide);
                if (model.Status == true)
                {
                    TechnicalDao.Instance.SaveExchange(id);
                    return RedirectToAction("Exchange");
                }
                else
                {
                    return RedirectToAction("Login", "Management");
                }
            }
            else
            {
                return RedirectToAction("Login", "Management");
            }
        }



    }
}
