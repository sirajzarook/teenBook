using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace teenBook.Data
{

    public interface IGenericRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity GetById(TKey id);
        Task<IEnumerable<TEntity>> GetAll();
        bool Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        bool Save();
    }

}