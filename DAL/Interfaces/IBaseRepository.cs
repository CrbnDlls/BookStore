using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<T> Create(T entity);
        public Task<bool> Delete(int id);
        public Task<T> Update(T entity);
        public Task<IEnumerable<T>> Select();
        public Task<T> GetById(int id);

    }
}
