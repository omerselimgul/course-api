using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBaseService <T> where T : class
    {
        public T Add(T entity);
        public T Delete(T entity);
        public T Update(T entity);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> Get(T entity);
    }
}
