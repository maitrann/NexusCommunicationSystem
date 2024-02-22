using Nexus.Models.Dao;
using Nexus.Models.ModelView;

namespace Nexus.Models.Repository
{
    public class NexusRepository
    {
        private static NexusRepository instance;
        private NexusRepository() { }
        public static NexusRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NexusRepository();
                }
                return instance;
            }
        }


        //storage
        public List<StorageView> GetOrderExpired(int id)
        {
            return StorageDao.Instance.getOrderExpired(id);
        }

        public List<StorageView> getOrderUsing(int id)
        {
            return StorageDao.Instance.getOrderUsing(id);
        }
        public List<StorageView> getOrderCancel(int id)
        {
            return StorageDao.Instance.getOrderCancel(id);
        }
        public List<StorageView> getOrderRequest(int id)
        {
            return StorageDao.Instance.getOrderRequest(id);
        }




        public List<StorageView> GetOrders(int id)
        {
            return StorageDao.Instance.getDataWithId(id);
        }



        //Blog
        public List<Blog> GetBlogs()
        {
            return BlogDao.Instance.getAllData();
        }
        public Blog GetDetailBlog(int id)
        {
            return BlogDao.Instance.detail(id);
        }
        public List<Blog> GetBlogTop4()
        {
            return BlogDao.Instance.getDataTop4();
        }
        #region SearchStore
        public List<District> GetDistricts()
        {
            return SearchStoreDao.Instance.GetDistricts();
        }
        public List<SearchStoreView> GetStores(int id)
        {
            return SearchStoreDao.Instance.getStore(id);
        }
        public List<SearchStoreView> GetSto()
        {
            return SearchStoreDao.Instance.getSto();
        }

        #endregion


        #region ServicePlan
        public bool insertExPainting(ServicePlanView ev)
        {
            return ServicePlanDao.Instance.insertData(ev);
        }

        //public List<ServicePlanView> getArtsOfExbition(int idExhibtion)
        //{
        //    return ServicePlanDao.Instance.getArtsOfExbition(idExhibtion);
        //}

        public bool updateExPaint(ServicePlanView ev)
        {
            return ServicePlanDao.Instance.updateData(ev);
        }

        public bool deleteExPaint(ServicePlanView ev)
        {
            return ServicePlanDao.Instance.deleteData(ev);
        }

        public List<ServicePlanView> getCatetory()
        {
            return ServicePlanDao.Instance.getCategory();
        }
        public List<ExpiryDate> GetExpiryDates()
        {
            return ServicePlanDao.Instance.GetExpiryDates();
        }
        public List<ServicePlanView> getPaintToShow()
        {
            return ServicePlanDao.Instance.getAllData();
        }
        public List<ServicePlanView> SearchExpiry(int idExpiry)
        {
            return ServicePlanDao.Instance.SearchExpiryDate(idExpiry);
        }

        public List<ServicePlanView> getPaintToShowWithCate(int id)
        {
            return ServicePlanDao.Instance.getAllDataWithCate(id);
        }

        public List<ServicePlanView> getPaintToShowBestSeller()
        {
            return ServicePlanDao.Instance.getAllDataBestSeller();
        }

        public List<ServicePlanView> getPaintToShowSpecial()
        {
            return ServicePlanDao.Instance.getAllDataSpecial();
        }

        public ServicePlanView getPaintToShowbyID(ServicePlanView idExPaint)
        {
            return ServicePlanDao.Instance.getData(idExPaint);
        }
        public ServicePlanView GetServiceByID(int id)
        {
            return ServicePlanDao.Instance.getDataWithID(id);
        }
        #endregion


        #region Customer
        public bool checkExistsPhone(string phone)
        {
            return CustomerDao.Instance.checkExistsPhone(phone);
        }

        public bool customerRegister(CustomerView cv)
        {
            return CustomerDao.Instance.insertData(cv);
        }

        public CustomerView getCustomer(CustomerView cv)
        {
            return CustomerDao.Instance.getData(cv);
        }
        public CustomerView checkLogin(string phone, string password)
        {
            return CustomerDao.Instance.checkLogin(phone, password);
        }

        public void changeProfile (int  id, string name , string email, string phone)
        {
             StorageDao.Instance.changeProfile(id, name , email, phone);
        }
        public int changePass(int id, string newPass, string oldPass)
        {
             return StorageDao.Instance.changePassword(id, newPass,oldPass);
        }
        public CustomerView GetCus(int id)
        {
            return StorageDao.Instance.getCus(id);
        }

        #endregion

        #region CusBuying
        public CusBuyingView getServicePlan(int id, int quantity, int expiry)
        {
            return CusBuyingDao.Instance.getServicePlan(id, quantity, expiry);
        }
        public dynamic getExpiryDate(int id)
        {
            return CusBuyingDao.Instance.GetExpiryDate(id);
        }
        public bool insertCusbuying(CusBuyingView cv)
        {
            return CusBuyingDao.Instance.insertData(cv);
        }

        public List<CusBuyingView> ListCusBuying(int idCus)
        {
            return CusBuyingDao.Instance.ListCusBuying(idCus);
        }

        public Models.Order InsertOrder(int ServicePlanID, int CustomerID, int EmployeeID, int ExpiryDateID,
            DateTime OrderDate, string Name, string Phone, string Address, int Quantity, decimal TheAmountPaid,
            decimal Total, bool StatusPaid, int StatusService)
        {
            return CusBuyingDao.Instance.InsertOrder(ServicePlanID, CustomerID, EmployeeID, ExpiryDateID,
            OrderDate, Name, Phone, Address, Quantity, TheAmountPaid,
             Total, StatusPaid, StatusService);
        }
        #endregion

        #region Bill
        public void InsertBill(string IDPayment, string OrderID, DateTime RegistrationDate)
        {
            BillDao.Instance.InsertBill(IDPayment, OrderID, RegistrationDate);
        }
        #endregion

        #region ProductExchange
        public void InsertChange(int billID, int quantityExchange, string reason)
        {
            StorageDao.Instance.insertChange(billID, quantityExchange, reason);
        }
        public List<ProductExchange> GetProChange()
        {
            return StorageDao.Instance.getProChange();
        }
        public void cancelSP(int id)
        {
            StorageDao.Instance.cancelSP(id);
        }
        #endregion
    }
}
