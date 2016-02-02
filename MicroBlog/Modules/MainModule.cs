using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MicroBlog.Models;
using Nancy;
using Nancy.Routing;
using Nancy.ModelBinding;

namespace MicroBlog.Modules
{
    public class MainModule : NancyModule
    {


        public MainModule()
        {
            
            // Routes simply return a view associated with the request
            // Simple Login, view, write. 
            // Everything else is simple and self explanatory to understand.
            
            
            Get["/"] = o =>

            {
                var title = new ShowTitle {Title = "Read"};
                return View["Views/Home", title];

            };
            Get["/write"] = o =>

            {
                var title = new ShowTitle {Title = "Write"};
                return View["Views/Write", title];

            };
            Get["/login"] = o =>

            {
                var title = new ShowTitle {Title = "Login"};
                return View["Views/Login", title];

            };


        }
    }
}