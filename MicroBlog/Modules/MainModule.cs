using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MicroBlog.Models;
using Nancy;
using Nancy.Routing;
using Nancy.ModelBinding;
using Nancy.ViewEngines;

namespace MicroBlog.Modules
{
    public class MainModule : NancyModule
    {


        public MainModule()
        {

            // Routes simply return a view associated with the request
            // Simple Login, view, write. 
            // Everything else is simple and self explanatory to understand.


            //Todo Maybe put all routes into their own file/model if it gets big enough

            //Get Requests
            Get["/"] = Home;

            Get["/write"] = Write;
            Get["/login"] = Login;

            //Post Requests
            Put["/{id:int}"] = parameters =>
            {
                return HttpStatusCode.NotImplemented;
            };

            Delete["/{id:int}"] = x =>
            {
                return HttpStatusCode.NotImplemented;
            };

        }

        //Actions Methods here: So the contructor doesn't get bloated.


        //Nancy will look for a razor file with  a file name that matches the class name of the viewmodel.
        private dynamic Home(dynamic o)
        {

            var _post = new Post();
            _post.Title = o.title;
            _post.Content = o.content;


                return View["Views/Pages/Home", _post];
           

        }

        private dynamic Write(dynamic o)
        {
       
                return View["Views/Pages/Write"];
          
           
        }

        private dynamic Login(dynamic o)
        {
            return View["Views/Pages/Login"];
        }





    }
}