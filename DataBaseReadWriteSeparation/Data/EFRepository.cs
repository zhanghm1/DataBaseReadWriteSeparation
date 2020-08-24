using DatabaseChoose;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataBaseReadWriteSeparation.Data
{
    public class EFRepository<T> where T : class
    {
        private readonly TestDbcontext _context;
        private readonly DbSet<T> _dbset;

        public EFRepository(TestDbcontext context)
        {
            this._context = context;
            this._dbset = context.Set<T>();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            return await _dbset.Where(where).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> where)
        {
            return await _dbset.Where(where).ToListAsync();
        }

        public async Task<bool> AddAsync(T t)
        {
            await _dbset.AddAsync(t);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}