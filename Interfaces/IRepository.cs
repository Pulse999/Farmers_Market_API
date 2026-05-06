namespace Farmers_Market_API.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T item);

        List<T> GetAll();

        T? GetById(int id);

        bool Delete(int id);
    }
}