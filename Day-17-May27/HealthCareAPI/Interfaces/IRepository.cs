using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareAPI.Interfaces
{
    public interface IRepository<K, T> where T : class
    {
        T GetById(K id);
        T Add(T item);
        T Update(T item);
        K Delete(K id);

        ICollection<T> GetAll();
    }
}