using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using teenBook.Data;
using teenBook.Models.ViewModels;

namespace teenBook.Controllers
{
    public class ManageReplyController : Controller
    {
        ITeenbookRepository _repo;

        public ManageReplyController(ITeenbookRepository repo)
        {
            _repo = repo;
        }



        // GET: TestReply/Create
        public ActionResult Create()
        {
            ReplyViewModel model = new ReplyViewModel();
            model.Topics = _repo.GetTopics().ToList().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            });


            return View(model);
        }

        // POST: TestReply/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body,Created,TopicId")] ReplyViewModel replyViewModel)
        {
            if (ModelState.IsValid)
            {
                Reply reply = new Reply
                {
                    //Id = replyViewModel.Id,
                    Title = replyViewModel.Title,
                    Body = replyViewModel.Body,
                    Created = replyViewModel.Created,
                    TopicId = replyViewModel.TopicId
                };
                _repo.AddReply(reply);
                _repo.Save();

                replyViewModel.Topics = _repo.GetTopics().ToList().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Title
                });

                //return RedirectToAction("Index");
            }

            return View(replyViewModel);
        }


    }
}
