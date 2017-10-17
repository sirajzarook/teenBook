using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using teenBook.Data;

namespace teenBook.Controllers
{
    public class TopicsController : ApiController
    {
        ITeenbookRepository _repo;

        public TopicsController(ITeenbookRepository repo)
        {
            _repo = repo;
        }

        //Making Async calls to webapi
        // .../api/v1/topics
        // .../api/v1/topics?includereplies=true
        [ResponseType(typeof(IEnumerable<Topic>))]
        public async Task<IHttpActionResult> Get(bool IncludeReplies = false)
        {
            IEnumerable<Topic> results;
            if (IncludeReplies == true)
            {
                results = await _repo.GetTopicIncludingRepliesAsync();
            }
            else
            {
                results = await _repo.GetTopicsAsync();

            }
            var topics = results.OrderByDescending(t => t.Created)
                .Take(25)
                .ToList();


            return Ok(topics);
        }


        //// .../api/v1/topics
        //// .../api/v1/topics?includereplies=true
        //public IEnumerable<Topic> Get(bool IncludeReplies = false)
        //{
        //    IQueryable<Topic> results;
        //    if (IncludeReplies == true)
        //    {
        //        results = _repo.GetTopicsIncludingReplies();
        //    }
        //    else
        //    {
        //        results = _repo.GetTopics();

        //    }
        //    var topics = results.OrderByDescending(t => t.Created)
        //        .Take(25)
        //        .ToList();


        //    return topics;
        //}

        public HttpResponseMessage Post([FromBody]Topic newTopic)
            {
            if (newTopic.Created == default(DateTime))
                newTopic.Created = DateTime.UtcNow;

            newTopic.QuestionnaireId = "01549318-6C6E-4C65-A2E5-94662509970E"; //TODO
            newTopic.UserId = "TODO_UserID"; 

            if (_repo.AddTopic(newTopic) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newTopic);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
    }
}
