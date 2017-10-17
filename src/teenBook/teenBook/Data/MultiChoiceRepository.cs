using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace teenBook.Data
{
    public class MultiChoiceRepository : EntityFrameworkRepository<MultiChoice, int>, IMultiChoiceRepository
    {
        private readonly TeenbookContext _dbContext;
        public MultiChoiceRepository(TeenbookContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MultiChoice>> GetMultiChoiceByTopicIdAsync(int id)
        {

            //try
            //{
            return await _dbContext.MultiChoices.Where(t => t.TopicId == id).ToListAsync();
            //}
            //catch (Exception ex)
            //{
            //    //TODO log error

            //}
        }

        public bool Save()
        {
            try
            {
                return _dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                //TODO log error
                return false;
            }
        }


    }
}