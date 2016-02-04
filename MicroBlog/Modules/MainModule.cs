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
               
                return View["Views/Pages/Home"];

            };
            Get["/write"] = o =>

            {
                
                return View["Views/Pages/Write"];

            };
            Get["/login"] = o =>

            {
                
                return View["Views/Pages/Login"];

            };


        }
    }
}