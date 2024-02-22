using Nexus.Models.ModelView;

namespace Nexus.Models.Dao
{
    public class SearchStoreDao
    {
        private static SearchStoreDao? instance = null;
        private SearchStoreDao() { }
        public static SearchStoreDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SearchStoreDao();
                }
                return instance;
            }
        }

        public List<District> GetDistricts()
        {
            var en = new NexusContext();
            var result = en.Districts.ToList();
            return result;
        }
        public List<SearchStoreView> getStore(int id)
        {
            var en = new NexusContext();
            var result = en.Stores.Where(x => x.DistrictID == id && x.Status == true)
                .Select(x => new SearchStoreView() { Id = x.Id, Name = x.Name, DistrictName = x.StoreDistrict.Name, Ward = x.Ward }).ToList();
            return result;
        }
        public List<SearchStoreView> getSto()
        {
            var en = new NexusContext();
            var result = en.Stores.Where(x => x.Status == true)
                .Select(x => new SearchStoreView() { Id = x.Id, Name = x.Name, DistrictName = x.StoreDistrict.Name, Ward = x.Ward }).ToList();
            return result;
        }
    }
}
