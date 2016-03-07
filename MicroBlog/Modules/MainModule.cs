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


          // this.RequiresAuthentication(). Make that function after everything works properly

          //Requests will call these methods
          Get["/"] = Home;
            Get["post/write"] = Write_GET;
            Post["post/write"] = Write_POST;
            Get["/login"] = Login;

            //I better make it async so the web app can continue while it's being updated in the background
            //TODO insert success or failure message
            Put["post/edit/{id:int}"] = parameters =>
            {
                return HttpStatusCode.NotImplemented;
            };

            //Note: This line made me include Microsoft.CSharp as a reference.
            Delete["post/delete/{id:int}"] = param =>
            {
                int id = param.id;
                var result = _post.Delete(id);
                return (result) ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            };

        }

        //Actions Methods here: So the contructor doesn't get bloated.
        //Nancy will look for a razor file with  a file name that matches the class name of the viewmodel.
        private dynamic Home(dynamic o)
        {
            List<Post> postList = _post.GetAll();
                return View["Views/Pages/Home.cshtml", postList];
        }

        private dynamic Write_GET(dynamic o)
        {
            var post = new Post();
                return View["Views/Pages/Write", post];
        }

        public dynamic Write_POST(dynamic o)
        {
            //Binds model to view
            var post = this.Bind<Post>();
            _post.Create(post);
            return Response.AsRedirect("/");
        }

        private dynamic Login(dynamic o)
        {
            return View["Views/Pages/Login"];
        }

    }
}