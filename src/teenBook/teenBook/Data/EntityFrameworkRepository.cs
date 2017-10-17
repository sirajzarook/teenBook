using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace teenBook.Data
{
    public class EntityFrameworkRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbContext _dbContext;
        public EntityFrameworkRepository(DbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            _dbContext = dbContext;
        }
        protected DbContext DbContext
        {
            get { return _dbContext; }
        }
        public bool Create(TEntity entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                DbContext.Set<TEntity>().Add(entity);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                return false;
            }

        }
        public TEntity GetById(TKey id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                DbContext.Set<TEntity>().Attach(entity);
                DbContext.Set<TEntity>().Remove(entity);
                return true;
            }
            catch
            {
                //TODO log error
                return false;
            }
        }
        public bool Update(TEntity entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                DbContext.Set<TEntity>().Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch
            {
                //TODO log error
                return false;
            }

        }
    }
}