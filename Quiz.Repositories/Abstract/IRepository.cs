using Quiz.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repositories.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<IDataResult<List<T>>> GetListAsync(Expression<Func<T, bool>> filter = null);
        Task<IDataResult<T>> AddAsync(T entity);
        Task<IDataResult<T>> UpdateAsync(T entity);
        Task<IDataResult<T>> DeleteAsync(T entity);
        Task<IDataResult<T>> GetAsync(Expression<Func<T, bool>> filter);

    }
}
