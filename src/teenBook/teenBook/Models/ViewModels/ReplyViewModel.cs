using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teenBook.Data;

namespace teenBook.Models.ViewModels
{
    public class ReplyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }

        public int TopicId { get; set; }
        public IEnumerable<SelectListItem> Topics { get; set; }
    }
}