using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teenBook.Data;    

namespace teenBook.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private ITeenbookRepository _repo;

        public HomeController(ITeenbookRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index()
        {
            var topics = _repo.GetTopics()
                .OrderByDescending(t => t.Created)
                .Take(25)
                .ToList();
            return View(topics);
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}