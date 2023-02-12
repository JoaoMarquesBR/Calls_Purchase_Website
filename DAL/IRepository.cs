using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDAL
{
    public interface IRepository<T>
    {

        Task<List<T>> GetAll();
        Task<T> Add(T entity);
        Task<int> Delete(int i);
        Task<int> Update(T enity);


    }
}
