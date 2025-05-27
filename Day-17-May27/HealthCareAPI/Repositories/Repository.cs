using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Exceptions;
using HealthCareAPI.Interfaces;

namespace HealthCareAPI.Repositories
{
    public abstract class Repository<K, T> : IRepository<K, T> where T : class
    {
        protected List<T> _items = new List<T>();
        protected abstract K GenerateId();

        public abstract T GetById(K id);

        public abstract ICollection<T> GetAll();
        public T Add(T item)
        {
            var id = GenerateId();
            var property = typeof(T).GetProperty("Id");

            if (property != null)
            {
                property.SetValue(item, id);
            }

            if (_items.Contains(item))
            {
                throw new DuplicateEntityException("Item is Already Present");
            }
            _items.Add(item);
            return item;
        }

        public K Delete(K id)
        {
            var item = GetById(id);
            if (item == null)
            {
                throw new KeyNotFoundException("Item not found");
            }

            _items.Remove(item);
            return id;
        }

        public T Update(T item)
        {
            var old_item = GetById((K)typeof(T).GetProperty("Id").GetValue(item)) ?? throw new KeyNotFoundException("Item is not present");
            var idx = _items.IndexOf(old_item);
            _items[idx] = item;
            return item;
        }



    }
}