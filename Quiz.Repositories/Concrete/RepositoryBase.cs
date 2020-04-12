using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quiz.Repositories.Abstract;
using Quiz.Results.Abstract;
using Quiz.Results.Concrete;

namespace Quiz.Repositories.Concrete
{
    public class RepositoryBase<TContext, TEntity> : IRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
    {
        TContext _context;
        public RepositoryBase(TContext context)
        {
            _context = context;
        }
        public async Task<IDataResult<List<TEntity>>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var response = new DataResult<List<TEntity>>();
            using (_context)
            {
                if (filter == null)
                {
                    var result = await _context.Set<TEntity>().ToListAsync();
                    response.Successeded = true;
                    response.Data = result;
                }
                else
                {
                    var result = await _context.Set<TEntity>().Where(filter).ToListAsync();
                    response.Successeded = true;
                    response.Data = result;
                }
            }
            return response;
        }
        public async Task<IDataResult<TEntity>> AddAsync(TEntity entity)
        {
            var response = new DataResult<TEntity>();
            var result = _context.Entry(entity);
            result.State =await Task.Run(()=> EntityState.Added);
            response.Successeded = true;
            response.Data = result.Entity;
            return response;
        }
        public async Task<IDataResult<TEntity>> UpdateAsync(TEntity entity)
        {
            var response = new DataResult<TEntity>();
            var result = _context.Entry(entity);
            result.State = await Task.Run(()=>EntityState.Modified);
            response.Successeded = true;
            response.Data = result.Entity;
            return response;
        }
        public async Task<IDataResult<TEntity>> DeleteAsync(TEntity entity)
        {
            var response = new DataResult<TEntity>();
            var result = _context.Entry(entity);
            result.State = await Task.Run(() => EntityState.Deleted);
            response.Successeded = true;
            response.Data = result.Entity;
            return response;
        }
    }
}
