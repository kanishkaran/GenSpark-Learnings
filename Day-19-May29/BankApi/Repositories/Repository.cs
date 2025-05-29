using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Contexts;
using BankApi.Interfaces;

namespace BankApi.Repositories
{
    public abstract class Repository<K, T> : IRepository<K, T> where T : class
    {
        protected readonly BankingDbContext _context;

        public Repository(BankingDbContext context)
        {
            _context = context;
        }
        public abstract Task<T> GetById(K id);

        public abstract Task<IEnumerable<T>> GetAll();
        public async Task<T> Add(T item)
        {

            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<T> Update(K id, T item)
        {
            var old_item = await GetById(id) ?? throw new KeyNotFoundException("Item not found");
            _context.Entry(old_item).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
            return item;
        }

    }
}