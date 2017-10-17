using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace teenBook.Data
{
    public class Topic
    {
        public string QuestionnaireId { get; set; }
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        [DefaultValue("1")]
        public int Type { get; set; }
        [DefaultValue("1")]
        public int AgeGroup { get; set; }

        public ICollection<Reply> Replies { get; set; }

    }
}