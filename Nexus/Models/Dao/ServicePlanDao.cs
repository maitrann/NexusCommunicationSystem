using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nexus.Models.Interface;
using Nexus.Models.ModelView;

namespace Nexus.Models.Dao
{
    public class ServicePlanDao : IDataRepository<ServicePlanView>
    {
        private static ServicePlanDao? instance = null;
        private ServicePlanDao() { }
        public static ServicePlanDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServicePlanDao();
                }
                return instance;
            }
        }

        public bool deleteData(ServicePlanView modelDelete)
        {
            throw new NotImplementedException();
        }
        public List<ServicePlanView> getCategory()
        {
            var en = new NexusContext();
            var result = en.Categories
                .Select(x => new ServicePlanView()
                {
                    CategoryID = x.Id,
                    CategoryName = x.Name,
                }).ToList();
            return result;
        }
        public List<ExpiryDate> GetExpiryDates()
        {
            var en = new NexusContext();
            var result = en.ExpiryDates.ToList();
            return result;
        }
        public List<ServicePlanView> getAllData()
        {
            var en = new NexusContext();
            var result = en.ServicePlans.Where(x => x.Status == true)
                .Select(x => new ServicePlanView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image,
                    Options = x.Options,
                    OptionsID = 0
                }).ToList();
            return result;
        }
        public List<ServicePlanView> SearchExpiryDate(int idExpiry)
        {
            var en = new NexusContext();
            var result = en.ServicePlans.Where(x => x.Status == true)
                .Select(x => new ServicePlanView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image,
                    Options = x.Options,
                    OptionsID = 0
                }).ToList();
            List<ServicePlanView> res = new List<ServicePlanView>();
            for (int i = 0; i < result.Count; i++)
            {
                var Option = JsonConvert.DeserializeObject<List<Option>>(result[i].Options ?? "");
                foreach (Option item in Option)
                {
                    result[i].OptionsID = item.ID;
                    if (result[i].OptionsID == idExpiry)
                    {
                        res= result.Where(x => x.OptionsID == idExpiry).ToList();
                        break;
                    }
                }
            }
            return res;
        }
        public List<ServicePlanView> getAllDataWithCate(int id)
        {
            var en = new NexusContext();
            var result = en.ServicePlans.Where(x => x.Status == true && x.CategoryID == id)
                .Select(x => new ServicePlanView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image,
                }).ToList();
            return result;
        }

        public List<ServicePlanView> getAllDataBestSeller()
        {
            var en = new NexusContext();
            var result = en.ServicePlans.Where(x => x.Status == true)
                .Select(x => new ServicePlanView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image,
                }).OrderByDescending(y=>y.Price).Take(6).ToList();
            return result;
        }

        public List<ServicePlanView> getAllDataSpecial()
        {
            var en = new NexusContext();
            var result = en.ServicePlans.Where(x => x.Status == true)
                .Select(x => new ServicePlanView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image,
                }).OrderByDescending(y => y.Price).Reverse().Take(6).ToList();
            return result;
        }

        public ServicePlanView getData(ServicePlanView model)
        {
            var en = new NexusContext();
            var result = en.ServicePlans.Where(x => x.Status == true && x.Id == model.Id)
                .Select(x => new ServicePlanView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image,
                    Description = x.Description
                }).FirstOrDefault();
            return result;
        }

        public ServicePlanView getDataWithID(int id)
        {
            var en = new NexusContext();
            var result = en.ServicePlans.Where(x => x.Status == true && x.Id == id)
                .Select(x => new ServicePlanView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image,
                    Description = x.Description,
                    CategoryID = x.CategoryID
                }).FirstOrDefault();
            return result;
        }

        public bool insertData(ServicePlanView modelInsert)
        {
            throw new NotImplementedException();
        }

        public bool updateData(ServicePlanView modelUpdate)
        {
            throw new NotImplementedException();
        }


    }
}
