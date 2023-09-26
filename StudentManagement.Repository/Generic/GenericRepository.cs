using Microsoft.EntityFrameworkCore;
using StudentManagement.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Repository.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly StudentContext _context;
        private DbSet<T> _dbSet;
        public GenericRepository(StudentContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<T>();
        }

        public async Task<List<T>> Get()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> Get(object id)
        {
            var _result = await _dbSet.FindAsync(id);
            return _result;
        }

        public async Task<T> Add(T obj)
        {
            await _dbSet.AddAsync(obj);
            if (await Save() > 0)
                return obj;
            return null;
        }

        public async Task<T> Update(T obj)
        {
            _dbSet.Update(obj);
            if (await Save() > 0)
                return obj;
            return null;
        }

        public async Task<T> Delete(object id)
        {
            var _result = await Get(id);
            _dbSet.Remove(_result);
            if (await Save() > 0)
                return _result;
            return null;
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
