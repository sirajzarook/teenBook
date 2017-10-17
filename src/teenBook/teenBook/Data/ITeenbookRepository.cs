using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teenBook.Data
{
    public interface ITeenbookRepository
    {

        Task<IEnumerable<Topic>> GetTopicsAsync();

        Task<IEnumerable<Topic>> GetTopicIncludingRepliesAsync();
        IQueryable<Topic> GetTopics();
        IQueryable<Topic> GetTopicsIncludingReplies();

        IQueryable<Reply> GetRepliesByTopics(int topicId);

        bool Save();

        bool AddTopic(Topic newTopic);
        bool AddReply(Reply newReply);


    }
}
