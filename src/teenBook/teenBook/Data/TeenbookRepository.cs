using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace teenBook.Data
{
    public class TeenbookRepository : ITeenbookRepository
    {
        TeenbookContext _ctx;

        public TeenbookRepository(TeenbookContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Topic>> GetTopicsAsync()
        {
            return await _ctx.Topics.ToListAsync();
        }

        public async Task<IEnumerable<Topic>> GetTopicIncludingRepliesAsync()
        {
            return await _ctx.Topics.Include("Replies").ToListAsync();
        }

        public IQueryable<Topic> GetTopics()
        {
            return  _ctx.Topics;
        }

        public IQueryable<Reply> GetRepliesByTopics(int topicId)
        {
            return _ctx.Replies.Where(t => t.TopicId == topicId);
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                //TODO log error
                return false;
            }
        }

        public bool AddTopic(Topic newTopic)
        {

            try
            {
                _ctx.Topics.Add(newTopic);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IQueryable<Topic> GetTopicsIncludingReplies()
        {
            return _ctx.Topics.Include("Replies");
        }

        public bool AddReply(Reply newReply)
        {
            try
            {
                _ctx.Replies.Add(newReply);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}