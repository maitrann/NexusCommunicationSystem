using Microsoft.AspNetCore.WebUtilities;
using Nexus.Helper;
using Nexus.Models.Interface;
using Nexus.Models.ModelView;
using System.Runtime.InteropServices;

namespace Nexus.Models.Dao
{
    public class StorageDao : IDataRepository<StorageView>
    {
        private static StorageDao? instance = null;
        private StorageDao() { }
        public static StorageDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StorageDao();
                }
                return instance;
            }
        }

        public bool deleteData(StorageView modelDelete)
        {
            throw new NotImplementedException();
        }

        public List<StorageView> getAllData()
        {

            throw new NotImplementedException();
        }



        public List<StorageView> getOrderExpired(int id)
        {
            var en = new NexusContext();
            var sv = (from b in en.Bills
                      join o in en.Orders
                      on Convert.ToInt32(b.OrderID) equals o.Id
                      join s in en.ServicePlans
                      on o.ServicePlanID equals s.Id
                      join e in en.ExpiryDates
                      on o.ExpiryDateID equals e.ID
                      join c in en.Categories
                      on s.CategoryID equals c.Id

                      where o.CustomerID == id && o.StatusService == 2
                      select new StorageView
                      {
                          IdBill = b.Id,
                          OrderID = o.Id,
                          IdCategory = c.Id,
                          NameCategory = c.Name,

                          IdServicePlan = s.Id,
                          NameService = s.Name,
                          OrderDate = o.OrderDate.ToString(),
                          Quantity = o.Quantity,
                          ExpiryDate = e.Name,
                          Total = o.Total,
                          StatusPaid = o.StatusPaid,

                          Name = o.Name,
                          Address = o.Address,
                          Phone = o.Phone,

                          TheAmountPaid = o.TheAmountPaid,
                          StatusService = o.StatusService,

                          ConnectionDate = b.ConnectionDate.ToString(),
                          ExpirationDate = b.ExpirationDate.ToString(),
                      }).OrderByDescending(x => x.IdBill).ToList();
            return sv;
        }
        public List<StorageView> getOrderUsing(int id)
        {
            var en = new NexusContext();
            var sv = (from b in en.Bills
                      join o in en.Orders
                      on Convert.ToInt32(b.OrderID) equals o.Id
                      join s in en.ServicePlans
                      on o.ServicePlanID equals s.Id
                      join e in en.ExpiryDates
                      on o.ExpiryDateID equals e.ID
                      join c in en.Categories
                      on s.CategoryID equals c.Id

                      where o.CustomerID == id && o.StatusService == 1 || o.CustomerID == id && o.StatusService == 0
                      select new StorageView
                      {
                          IdBill = b.Id,
                          OrderID = o.Id,
                          IdCategory = c.Id,
                          NameCategory = c.Name,

                          IdServicePlan = s.Id,
                          NameService = s.Name,
                          OrderDate = o.OrderDate.ToString(),
                          Quantity = o.Quantity,
                          ExpiryDate = e.Name,
                          Total = o.Total,
                          StatusPaid = o.StatusPaid,

                          Name = o.Name,
                          Address = o.Address,
                          Phone = o.Phone,

                          TheAmountPaid = o.TheAmountPaid,
                          StatusService = o.StatusService,

                          ConnectionDate = b.ConnectionDate.ToString(),
                          ExpirationDate = b.ExpirationDate.ToString(),
                      }).OrderByDescending(x => x.IdBill).ToList();
            return sv;
        }
        public List<StorageView> getOrderRequest(int id)
        {
            var en = new NexusContext();
            var sv = (from p in en.ProductExchanges
                      join b in en.Bills
                      on p.BillID equals b.Id
                      join o in en.Orders
                      on Convert.ToInt32(b.OrderID) equals o.Id
                      join s in en.ServicePlans
                      on o.ServicePlanID equals s.Id
                      join e in en.ExpiryDates
                      on o.ExpiryDateID equals e.ID
                      join c in en.Categories
                      on s.CategoryID equals c.Id


                      where o.CustomerID == id && p.Status != null
                      select new StorageView
                      {
                          IdBill = b.Id,
                          OrderID = o.Id,
                          IdCategory = c.Id,
                          NameCategory = c.Name,

                          IdServicePlan = s.Id,
                          NameService = s.Name,
                          OrderDate = o.OrderDate.ToString(),
                          Quantity = o.Quantity,
                          QuantityChange = p.QuantityChange,
                          ExpiryDate = e.Name,
                          Reason = p.Reason,
                          ExchangeDate = p.ExchangeDate.ToString(),
                          StatusChange = p.Status,
                         

                          Name = o.Name,
                          Address = o.Address,
                          Phone = o.Phone,
                      }).OrderByDescending(x => x.IdBill).ToList();
            return sv;
        }
        public List<StorageView> getOrderCancel(int id)
        {
            var en = new NexusContext();
            var sv = (from b in en.Bills
                      join o in en.Orders
                      on Convert.ToInt32(b.OrderID) equals o.Id
                      join s in en.ServicePlans
                      on o.ServicePlanID equals s.Id
                      join e in en.ExpiryDates
                      on o.ExpiryDateID equals e.ID
                      join c in en.Categories
                      on s.CategoryID equals c.Id

                      where o.CustomerID == id && o.StatusService == 3
                      select new StorageView
                      {
                          IdBill = b.Id,
                          OrderID = o.Id,
                          IdCategory = c.Id,
                          NameCategory = c.Name,

                          IdServicePlan = s.Id,
                          NameService = s.Name,
                          OrderDate = o.OrderDate.ToString(),
                          Quantity = o.Quantity,
                          ExpiryDate = e.Name,
                          Total = o.Total,
                          StatusPaid = o.StatusPaid,

                          Name = o.Name,
                          Address = o.Address,
                          Phone = o.Phone,

                          TheAmountPaid = o.TheAmountPaid,
                          StatusService = o.StatusService,

                          ConnectionDate = b.ConnectionDate.ToString(),
                          ExpirationDate = b.ExpirationDate.ToString(),
                      }).OrderByDescending(x => x.IdBill).ToList();
            return sv;
        }


        //
        public List<StorageView> getDataWithId(int id)
        {
            var en = new NexusContext();
            var sv = (from b in en.Bills
                      join o in en.Orders
                      on Convert.ToInt32(b.OrderID) equals o.Id
                      join s in en.ServicePlans
                      on o.ServicePlanID equals s.Id
                      join e in en.ExpiryDates
                      on o.ExpiryDateID equals e.ID
                      join c in en.Categories
                      on s.CategoryID equals c.Id


                      where o.CustomerID == id
                      select new StorageView
                      {
                          IdServicePlan = s.Id,
                          NameService = s.Name,
                          OrderDate = o.OrderDate.ToString(),
                          ExpiryDate = e.Name,
                          Quantity = o.Quantity,
                          TheAmountPaid = o.TheAmountPaid,
                          Total = o.Total,
                          StatusPaid = o.StatusPaid,
                          StatusService = o.StatusService,
                          Name = o.Name,
                          Address = o.Address,
                          Phone = o.Phone,
                          IdCategory = c.Id,
                          NameCategory = c.Name,
                          RegistrationDate = b.RegistrationDate.ToString(),
                          ConnectionDate = b.ConnectionDate.ToString(),
                          ExpirationDate = b.ExpirationDate.ToString(),
                          IdBill = b.Id,
                          OrderID = o.Id,
                      }).OrderByDescending(x=>x.IdBill).ToList();
            return sv;
        }
        public void insertChange(int billID, int quantityExchange, string reason)
        {
            var en = new NexusContext();
            ProductExchange exchange = new ProductExchange();
            exchange.BillID = billID;
            exchange.Status = false;
            exchange.ExchangeDate = null;
            exchange.QuantityChange = quantityExchange;
            exchange.Reason = reason;
            en.ProductExchanges.Add(exchange);
            en.SaveChanges();
        }
        public void cancelSP(int id)
        {
            var en = new NexusContext();
            Order order = en.Orders.Find(id);
            order.StatusService = 3;
            en.SaveChanges();
        }
        public List<ProductExchange> getProChange()
        {
            var en = new NexusContext();
            var result = en.ProductExchanges.ToList();
            return result;
        }
        public StorageView getData(StorageView modelGet)
        {
            throw new NotImplementedException();
        }

        public bool insertData(StorageView modelInsert)
        {
            throw new NotImplementedException();
        }

        public bool updateData(StorageView modelUpdate)
        {
            throw new NotImplementedException();
        }



        public CustomerView getCus(int id)
        {
            var en = new NexusContext();
            var result = en.Customers.Where(x => x.Id == id)
                .Select(x => new CustomerView()
                {
                    Name = x.Name,
                    Phone = x.Phone,
                    Email = x.Email,
                    Password = x.Password,
                }).FirstOrDefault();
            return result;
        }


        public void changeProfile(int id, string name, string email, string phone)     //changeprofile
        {
            var en = new NexusContext();
            //Employee emp = new Employee();
            Customer emp = en.Customers.Find(id);
            emp.Name = name;
            emp.Email = email;
            emp.Phone = phone;

            en.SaveChanges();
        }

        public int changePassword(int id, string newPass,string oldPass)
        {
            int i = 0;
            try
            {
                var en = new NexusContext();
                var myCus = en.Customers.Find(id);
                if (myCus.Id!=null)
                {
                    if (HashPassword.GetInstance.CheckPass(oldPass, myCus.Password ?? ""))
                    {
                        myCus.Password = HashPassword.GetInstance.HashPass(newPass);
                        en.SaveChanges();
                        i = 2;
                    }
                    else
                    {
                        i = 1;
                    }
                }
                else
                {
                    i = -1;
                }
            }
            catch (Exception)
            {
                i = -1;
            }
            return i;
        }


    }
}
