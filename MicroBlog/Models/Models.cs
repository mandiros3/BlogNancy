using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;

namespace MicroBlog.Models
{
    
    public class ShowTitle
    {
        public string Title { get; set; }
    }

    public class Note
    {
        public string title { get; set; }
        public string postContent { get; set; }
        public string date { get; set; }
      
    }

}