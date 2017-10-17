using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using teenBook.Data;

namespace teenBook.Controllers
{
    public class RepliesController : ApiController
    {
        ITeenbookRepository _repo;

        public RepliesController(ITeenbookRepository repo)
        {
            _repo = repo;
        }

        // .../api/v1/topics/topicid/replies
        public IEnumerable<Reply> Get(int topicid)
        {

            var replies = _repo.GetRepliesByTopics(topicid);

            return replies;
        }

        public HttpResponseMessage Post(int topicid, [FromBody]Reply newReply)
        {
            if (newReply.Created == default(DateTime))
                newReply.Created = DateTime.UtcNow;

            newReply.TopicId = topicid;

            if (_repo.AddReply(newReply) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, newReply);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
