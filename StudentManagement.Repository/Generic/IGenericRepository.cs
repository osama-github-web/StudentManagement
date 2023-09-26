using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Repository.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> Get();
        Task<T> Get(object id);
        Task<T> Add(T obj);
        Task<T> Update(T obj);
        Task<T> Delete(object id);
        Task<int> Save();
    }
}
