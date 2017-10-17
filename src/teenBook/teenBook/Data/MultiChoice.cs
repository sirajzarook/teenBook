using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace teenBook.Data
{
    public class MultiChoice
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Choice { get; set; }
        public int GUIOrder { get; set; }
    }
}