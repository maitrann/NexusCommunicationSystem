using Nexus.Models.Interface;
using Nexus.Models.Repository;

namespace Nexus.Models.Dao
{
    public class BlogDao : IDataRepository<Blog>
    {
        private static BlogDao instance;
        private BlogDao() { }
        public static BlogDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BlogDao();
                }
                return instance;
            }
        }

        public bool deleteData(Blog modelDelete)
        {
            throw new NotImplementedException();
        }

        public List<Blog> getAllData()
        {
            var en = new NexusContext();
            var result = en.Blogs
                .Select(x => new Blog()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Description = x.Description,
                }).ToList();
            return result;
        }

        public List<Blog> getDataTop4()
        {
            var en = new NexusContext();
            var result = en.Blogs
                .Select(x => new Blog()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Description = x.Description,
                }).Take(4).ToList();
            return result;
        }
        public Blog detail(int id)
        {
            var en = new NexusContext();
            var result = en.Blogs.Where(x => x.Id == id)
                .Select(x => new Blog()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Description = x.Description,
                }).FirstOrDefault();
            return result;
        }
        public Blog getData(Blog modelGet)
        {
            throw new NotImplementedException();

        }

        public bool insertData(Blog modelInsert)
        {
            throw new NotImplementedException();
        }

        public bool updateData(Blog modelUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
