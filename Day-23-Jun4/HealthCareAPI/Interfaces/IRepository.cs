using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareAPI.Interfaces
{
    public interface IRepository<K, T> where T : class
    {
        Task<T> GetById(K id);
        Task<T> Add(T item);
        Task<T> Update(K id,T item);
        Task<K> Delete(K id);

        Task<IEnumerable<T>> GetAll();
    }
}