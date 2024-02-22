
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Nexus.Helper;
using Nexus.Models.Interface;
using Nexus.Models.ModelView;
using System.Runtime.InteropServices;

namespace Nexus.Models.Dao
{
    public class CustomerDao : IDataRepository<CustomerView>
    {
        private static CustomerDao? instance = null;
        private CustomerDao() { }
        public static CustomerDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerDao();
                }
                return instance;
            }
        }

        public bool deleteData(CustomerView modelDelete)
        {
            throw new NotImplementedException();
        }

        public List<CustomerView> getAllData()
        {
            throw new NotImplementedException();
        }

        public CustomerView getData(CustomerView modelGet)
        {
            CustomerView modelOut = new CustomerView();
            try
            {
                var en = new NexusContext();
                var selCus = en.Customers.Where(w => w.Phone == modelGet.Phone).SingleOrDefault();
                if (selCus != null)
                {
                    modelOut.Id= selCus.Id;
                    modelOut.Password = selCus.Password;
                }
                else
                {
                    modelOut.Id = 0;
                }
            }
            catch (Exception)
            {

            }
            return modelOut;
        }

        public CustomerView checkLogin(string phone, string password)
        {

            var en = new NexusContext();
            CustomerView modelOut = new CustomerView();
            //HashPassword.GetInstance.CheckPass(password, cusLogin.Password)
            var selCus = en.Customers.Where(w => w.Phone == phone).SingleOrDefault();

            if (selCus != null)
            {
                modelOut.Id = selCus.Id;
                modelOut.Password= selCus.Password;
            }
            else
            {
                modelOut.Id = 0;
            }
            return modelOut;
        }
        public bool insertData(CustomerView modelInsert)
        {
            bool check = true;
            try
            {
                var en = new NexusContext();
                en.Customers.Add(new Customer
                {
                    Name= modelInsert.Name,
                    Phone= modelInsert.Phone,
                    Password= modelInsert.Password,
                    Email= modelInsert.Email,
                });
                en.SaveChanges();
            }
            catch (Exception)
            {

                check = false;
            }
            return check;
        }

        public bool updateData(CustomerView modelUpdate)
        {
            throw new NotImplementedException();
        }
        public bool checkExistsPhone(string phone)
        {
            bool check = true;
            try
            {
                var en = new NexusContext();
                check = en.Customers.Where(w =>w.Phone == phone).Any();
            }
            catch (Exception)
            {

                check = false;
            }
            return check;   
        }
    }
}
