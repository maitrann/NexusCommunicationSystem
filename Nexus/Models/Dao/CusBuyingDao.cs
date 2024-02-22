using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;
using Nexus.Models.Interface;
using Nexus.Models.ModelView;

namespace Nexus.Models.Dao
{
    public class CusBuyingDao : IDataRepository<CusBuyingView>
    {
        private static CusBuyingDao? instance = null;
        private CusBuyingDao() { }
        public static CusBuyingDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CusBuyingDao();
                }
                return instance;
            }
        }
        public bool deleteData(CusBuyingView modelDelete)
        {
            throw new NotImplementedException();
        }
        public dynamic GetExpiryDate(int id)
        {
            var en = new NexusContext();
            var result = en.ServicePlans.Where(x=>x.Id==id).Select(x => x.Options).FirstOrDefault();
            var Option = JsonConvert.DeserializeObject<List<Option>>(result??"");
            return Option;
        }
        public CusBuyingView getServicePlan(int id,int quantity,int expiry)
        {
            var en = new NexusContext();
            var expirySelect = en.ExpiryDates.Where(x => x.ID == expiry).Select(x => x.Name).FirstOrDefault();

            var result = en.ServicePlans.Where(x=>x.Id==id)
                .Select(x=> new CusBuyingView()
                {
                    ServicePlanID = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Price = x.Price,
                    Quantity = quantity,
                    ExpiryDateID = expiry,
                    Option = expirySelect,
                }).FirstOrDefault();
            return result;
        }
        public List<CusBuyingView> getAllData()
        {
            throw new NotImplementedException();
        }

        public CusBuyingView getData(CusBuyingView modelGet)
        {
            throw new NotImplementedException();
        }
        

        public bool insertData(CusBuyingView modelInsert)
        {
            bool check = true;
            try
            {
                var en = new NexusContext();
                en.Orders.Add(new Order
                {
                    CustomerID = modelInsert.CustomerID,
                    ServicePlanID = modelInsert.ServicePlanID,
                    //EmployeeID = modelInsert.EmployeeID,
                    OrderDate = DateTime.Today,

                });
                en.SaveChanges();
            }
            catch (Exception)
            {

               check = false;
            }
            return check;
        }

        public bool updateData(CusBuyingView modelUpdate)
        {
            throw new NotImplementedException();
        }
        public List<CusBuyingView> ListCusBuying(int CustomerID)
        {
            List<CusBuyingView> ls = new List<CusBuyingView>();
            try
            {
                var en = new NexusContext();
                ls = ( from a in en.Orders.Where(w => w.CustomerID == CustomerID)
                       select new CusBuyingView
                       {
                           OrderDate =  a.OrderDate,
                           
                       }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return ls;
        }

        public Order InsertOrder(int ServicePlanID, int CustomerID, int EmployeeID, int ExpiryDateID,
            DateTime OrderDate, string Name, string Phone, string Address, int Quantity, decimal TheAmountPaid,
            decimal Total, bool StatusPaid, int StatusService)
        {
            var ct = new NexusContext();
            Order order = new Order { 
                ServicePlanID = ServicePlanID,
                CustomerID = CustomerID,
                //EmployeeID = EmployeeID,
                ExpiryDateID = ExpiryDateID,
                OrderDate = OrderDate,
                Name = Name,
                Phone = Phone,
                Address = Address,
                Quantity = Quantity,
                TheAmountPaid = TheAmountPaid,
                Total = Total,
                StatusPaid = StatusPaid,
                StatusService = StatusService
            };
            ct.Add(order);
            ct.SaveChanges();
            return order;
        }
    }
}
