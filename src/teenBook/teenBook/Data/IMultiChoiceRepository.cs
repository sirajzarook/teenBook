using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace teenBook.Data
{
    public interface IMultiChoiceRepository : IGenericRepository<MultiChoice, int>
    {
        Task<IEnumerable<MultiChoice>> GetMultiChoiceByTopicIdAsync(int id);

    }
}