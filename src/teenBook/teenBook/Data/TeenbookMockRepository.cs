using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace teenBook.Data
{
    public class TeenbookMockRepository : ITeenbookRepository
    {

        private static readonly List<Topic> _topics = new List<Topic>();
        private static readonly List<Reply> _replies1 = new List<Reply>();
        private static readonly List<Reply> _replies2 = new List<Reply>();

        
        public TeenbookMockRepository()
        {

            _replies1.Add(new Reply
            {
                TopicId = 1,
                Id = 1,
                Title = "Title Topic 1 Reply id 1",
                Body = "Body Topic 1 Reply 1 text goes hear ",
                Created = DateTime.Parse("2010-09-01")
            });

            _replies1.Add(new Reply
            {
                TopicId = 2,
                Id = 2,
                Title = "Title topic 2 reply id 2",
                Body = "Body topic1 reply 2 text goes hear ",
                Created = DateTime.Parse("2010-09-01")
            });

            _topics.Add(new Topic
            {
                Id = 1,
                Title = "Title 1",
                Body = "Body 1 text goes hear ",
                Created = DateTime.Parse("2010-09-01"),

                Replies = _replies1.Where(t => t.Id == 1).ToArray()
               
            });

            _topics.Add(new Topic
            {
                Id = 2,
                Title = "Title 2",
                Body = "Body 2 text goes hear ",
                Created = DateTime.Parse("2010-09-01"),

                Replies = new List<Reply>
                {
                    new Reply() { Id = 3,
                            Title = "Title topic 2 reply id 3",
                            Body = "body text goes here",
                            Created = DateTime.Parse("2010-09-01")},

                    new Reply() { Id = 4,
                            Title = "Title topic 2 reply id 4",
                            Body = "body text goes here",
                            Created = DateTime.Parse("2010-09-01")}
                }
                

                
            });
        }
        public IQueryable<Reply> GetRepliesByTopics(int topicId)
        {
                        
            return _replies1.Where(t=> t.TopicId == topicId).ToList().AsQueryable();
        }

        public IQueryable<Topic> GetTopics()
        {
            return _topics.ToList().AsQueryable();
            
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool AddTopic(Topic newTopic)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Topic> GetTopicsIncludingReplies()
        {
            return _topics.ToList().AsQueryable();
        }

        public bool AddReply(Reply newReply)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Topic>> GetTopicsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Topic>> GetTopicIncludingRepliesAsync()
        {
            throw new NotImplementedException();
        }
    }
}