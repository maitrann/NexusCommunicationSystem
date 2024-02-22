namespace Nexus.Models.Interface
{
    public interface IDataRepository<T>
    {
        bool insertData(T modelInsert);
        bool updateData(T modelUpdate);
        bool deleteData(T modelDelete);
        List<T> getAllData();
        T getData(T modelGet);
    }
}
