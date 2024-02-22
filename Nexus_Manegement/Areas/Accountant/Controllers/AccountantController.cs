using ClosedXML;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Nexus_Management.Areas.Accountant.Dao;
using Nexus_Management.Areas.Accountant.ModelsView;
using System.Data;

namespace Nexus_Management.Areas.Accountant.Controllers
{
    [Area("Accountant")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class AccountantController : Controller
    {
        public IActionResult Index()
        {
            //session id
            if ((HttpContext.Session.GetString("accountantlogin")) == "accountantlogin")
            {
                ViewBag.SumCountOrder = AccountantDao.Instance.SumCountOrder();

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
            //session id
            if ((HttpContext.Session.GetString("accountantlogin")) == "accountantlogin")
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

        //public IActionResult ChangePass(string newPass)
        //{
        //    if ((HttpContext.Session.GetString("sessionlogin")) == "managementlogin")
        //    {
        //        int ide = ((int)HttpContext.Session.GetInt32("ide"));
        //        AccountantDao.Instance.changePassword(ide, newPass);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return Redirect("Management/Login");
        //    }
        //}
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
                int idCheck = AccountantDao.Instance.changePassword(ide, newPass, oldPass);
                return idCheck;
            }

        }

        public JsonResult GetRevenueChartJson()
        {
            var list = AccountantDao.Instance.GetRevenueChart();
            return Json(new { JSONList = list });
        }
        public JsonResult GetOrderChartJson()
        {
            var list = AccountantDao.Instance.GetOrderChart();
            return Json(new { JSONList = list });
        }
        public JsonResult GetPieChartJson()
        {
            var list = AccountantDao.Instance.PieChart();

            return Json(new { JSONList = list });
        }
        //
        public IActionResult Revenue()
        {
            if ((HttpContext.Session.GetString("accountantlogin")) == "accountantlogin")
            {
                ViewBag.Year = AccountantDao.Instance.GetYear();
                var model = AccountantDao.Instance.GetRevenue();
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Management");

            }

        }
        public JsonResult GetRevenueChartOfYearJson(string year)
        {
            var list = AccountantDao.Instance.GetRevenueChartOfYear(year);
            return Json(new { JSONList = list });
        }
        //
        public IActionResult ShowRevenueSelect(string year, string month, int? page)
        {
            if ((HttpContext.Session.GetString("accountantlogin")) == "accountantlogin")
            {
                var revenue = AccountantDao.Instance.GetRevenue();
                var revenueSelect = AccountantDao.Instance.GetRevenueWithSelect();
                if (!string.IsNullOrEmpty(year) && year != DateTime.Now.Year.ToString())
                {
                    if (!string.IsNullOrEmpty(month) && month != "all")
                    {
                        revenueSelect = revenueSelect.Where(x => x.OrderYear == year && x.OrderDate == month).ToList();
                        return PartialView("_RevenueMonth", revenueSelect);
                    }
                    else
                    {
                        revenueSelect = revenueSelect.Where(x => x.OrderYear == year).ToList();
                        return PartialView("_RevenueMonth", revenueSelect);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(month) && month != "all")
                    {
                        revenue = revenue.Where(x => x.OrderDate == month).ToList();
                        if (revenue.Count == 0)
                        {
                            TempData["Message"] = "No result for this month.";
                            return PartialView("_RevenueMonth", revenue);
                        }
                        else
                        {
                            return PartialView("_RevenueMonth", revenue);
                        }
                    }
                    else
                    {
                        return PartialView("_RevenueMonth", revenue);
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Management");

            }

        }

        public IActionResult ExportExcel(string year, string month)
        {
            if ((HttpContext.Session.GetString("accountantlogin")) == "accountantlogin")
            {
                int ide = ((int)HttpContext.Session.GetInt32("ide"));
                var model = AccountantDao.Instance.getEmp(ide);
                if (model.Status == true)
                {
                    List<OrderView> data;
                    var revenue = AccountantDao.Instance.GetRevenue();
                    var revenueSelect = AccountantDao.Instance.GetRevenueWithSelect();
                    if (!string.IsNullOrEmpty(year) && year != DateTime.Now.Year.ToString())
                    {
                        if (!string.IsNullOrEmpty(month) && month != "all")
                        {
                            revenueSelect = revenueSelect.Where(x => x.OrderYear == year && x.OrderDate == month).ToList();
                            data = revenueSelect;
                        }
                        else
                        {
                            revenueSelect = revenueSelect.Where(x => x.OrderYear == year).ToList();
                            data = revenueSelect;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(month) && month != "all")
                        {
                            revenue = revenue.Where(x => x.OrderDate == month).ToList();
                            data = revenue;
                        }
                        else
                        {
                            data = revenue;
                        }
                    }
                    using (var excel = new XLWorkbook())
                    {
                        var worksheet = excel.Worksheets.Add("Sheet1");
                        var currentRow = 1;
                        worksheet.Cell(currentRow, 1).Value = "IDBill";
                        worksheet.Cell(currentRow, 2).Value = "CustomerName";
                        worksheet.Cell(currentRow, 3).Value = "ServiceName";
                        worksheet.Cell(currentRow, 4).Value = "Price";
                        worksheet.Cell(currentRow, 5).Value = "Quantity";
                        worksheet.Cell(currentRow, 6).Value = "Usage Pack";
                        worksheet.Cell(currentRow, 7).Value = "Total";
                        worksheet.Cell(currentRow, 8).Value = "Registration Date";
                        worksheet.Cell(currentRow, 9).Value = "Connection Date";
                        worksheet.Cell(currentRow, 10).Value = "Expiration Date";


                        foreach (var item in data)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = item.IDBill;
                            worksheet.Cell(currentRow, 2).Value = item.CustomerName;
                            worksheet.Cell(currentRow, 3).Value = item.ServiceName;
                            worksheet.Cell(currentRow, 4).Value = item.Price;
                            worksheet.Cell(currentRow, 5).Value = item.Quantity;
                            worksheet.Cell(currentRow, 6).Value = item.UsagePack;
                            worksheet.Cell(currentRow, 7).Value = item.Total;
                            worksheet.Cell(currentRow, 8).Value = item.RegistrationDate;
                            worksheet.Cell(currentRow, 9).Value = item.ConnectionDate;
                            worksheet.Cell(currentRow, 10).Value = item.ExpirationDate;
                        }
                        using (var stream = new MemoryStream())
                        {
                            excel.SaveAs(stream);
                            var content = stream.ToArray();
                            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
                        }
                    }
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
