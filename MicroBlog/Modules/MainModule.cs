using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            //TODO insert success or failure message

            //The requests will call these methods
            //Homepage will shows first 3, but a /posts will show everything
            Get["/"] = Home;
            Get["/posts/new"] = newPost_GET;
            Post["/posts/new"] = newPost_POST;
            Get["/posts/{id:int}"] = getAPost;
            
            //Todo Have a separate Module for admin actions
            Get["/user/login"] = Login;
            //Note: This line made me include Microsoft.CSharp as a reference.
            Delete["/posts/{id:int}"] = Remove;

            //Todo Refactor this into a separate function just like the others.
            //Trying come up with content negotiation
            //Using POst for partial updates
            Post["/posts/{id:int}", true] = async (parameters, ctx) =>
            {
                //this.needs authentication
                var updatedPost = this.Bind<Post>();
                var item = await _post.Update(updatedPost);
                return Negotiate.WithModel(item).WithView("Views/Pages/Home");
            };
          

        }

        //Actions Methods here: So the contructor doesn't get bloated.
        //Nancy will look for a razor file with  a file name that matches the class name of the viewmodel.
        private dynamic Home(dynamic o)
        {
            List<Post> postList = _post.GetAll();
            return View["Views/Pages/Home.cshtml", postList];
        }
        private dynamic getAPost(dynamic o)
        {
            Post item = _post.Get(o.id);
            return item != null ? Response.AsJson(item) : HttpStatusCode.NotFound;
        }

        private dynamic newPost_GET(dynamic parameters)
        {
            var post = new Post();
            return View["Views/Pages/Write", post];
        }

        public dynamic newPost_POST(dynamic parameters)
        {
            //Binds model to view
            var post = this.Bind<Post>();
            _post.Create(post);
            return Response.AsRedirect("/");
        }

        public dynamic Remove(dynamic parameters)
        {
            int _id = parameters.id;
            var result = _post.Delete(_id);
            if (result == true)
            {
                return Response.AsRedirect("/");
            }
            else
            {
                return HttpStatusCode.NotFound;
            }
          

        }

        private dynamic Login(dynamic parameters)
        {
            return View["Views/Pages/Login"];
        }

    }
}