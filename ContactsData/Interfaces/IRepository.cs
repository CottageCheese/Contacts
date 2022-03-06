using System.Collections.Generic;

namespace ContactsData.Interfaces
{
    public interface IRepository<T>
    {
        bool Exists(int id);
        T Get(int id);
        List<T> Get();
        T Insert(T contact);
        T Update(T contact);
        bool Delete(int id);
    }
}