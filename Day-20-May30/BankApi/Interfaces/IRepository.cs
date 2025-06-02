using System;

namespace BankApi.Interfaces;


public interface IRepository<K, T> where T : class
{
    Task<T> GetById(K id);
    Task<T> Add(T item);
    Task<T> Update(K id, T item);
    Task<IEnumerable<T>> GetAll();

}
