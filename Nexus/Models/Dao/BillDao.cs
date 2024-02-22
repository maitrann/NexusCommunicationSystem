namespace Nexus.Models.Dao
{
    public class BillDao
    {
        private static BillDao instance;
        private BillDao() { }
        public static BillDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDao();
                }
                return instance;
            }
        }

        public void InsertBill(string IDPayment, string OrderID, DateTime RegistrationDate)
        {
            var ct = new NexusContext();
            var bill = new Bill
            {
                IDPayment = IDPayment,
                OrderID = OrderID,
                RegistrationDate = RegistrationDate,
                ConnectionDate = null,
                ExpirationDate = null,
            };
            ct.Add(bill);
            ct.SaveChanges();
        }
    }
}
