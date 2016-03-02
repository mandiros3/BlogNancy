using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MicroBlog.Models;
using Nancy;
using Nancy.Routing;
using Nancy.ModelBinding;
using Nancy.ViewEngines;
using MicroBlog.Interfaces;
using MicroBlog.DataProviders;
using Nancy.ViewEngines.Razor;

namespace MicroBlog.Modules
{
    public class MainModule : NancyModule
    {

        
        IRepository _post = new DataBaseRepository();

        public MainModule()
        {
            //Instantiate a new class that handles the database, implements the methods in the interface
         

          // Routes simply return a view associated with the request
          // Simple Login, view, write. 
          // Everything else is simple and self explanatory to understand.


          //Todo Maybe put all routes into their own file/model if it gets big enough

          //Get Requests
          Get["/"] = Home;
            Get["/write"] = Write;
            Post["/write"] = x =>
            {

                var post = this.Bind<Post>();
                _post.Create(post);
                return Response.AsRedirect("/");
            };
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
           
            List<Post> postList = _post.GetAll();
           

                return View["Views/Pages/Home.cshtml", postList];
        }

        private dynamic Write(dynamic o)
        {

            var post = new Post();
                return View["Views/Pages/Write", post];
        }

        private dynamic Login(dynamic o)
        {
            return View["Views/Pages/Login"];
        }





    }
}