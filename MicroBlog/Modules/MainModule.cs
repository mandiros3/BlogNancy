using System;
using System.Collections.Generic;
using MicroBlog.Models;
using Nancy;
using Nancy.ModelBinding;
using MicroBlog.Interfaces;
using MicroBlog.DataProviders;


namespace MicroBlog.Modules
{
    public class MainModule : NancyModule
    {

        IRepository _post = new DataBaseRepository();

        public MainModule()
        {  //Alpha version: 1. 
            //Todo Have a separate Module for admin actions
            Get["/"] = Home;
            Get["/posts/new"] = newPost_GET;
            Post["/posts/new"] = newPost_POST;
            Get["/posts/{id:int}"] = getAPost;
            Get["/user/login"] = Login;
            Delete["/posts/{id:int}"] = Remove;
            Post["/posts/{id:int}", true] = async (parameters, ctx) =>
            {
                //this.needs authentication
                var updatedPost = this.Bind<Post>();
                await _post.Update(updatedPost);
                return Response.AsRedirect("/");
            };
        }

        //Action Methods definitions (Called from the contructor)
        private dynamic Home(dynamic parameters)
        {
            List<Post> postList = _post.GetAll();
            return View["Views/Pages/Home.cshtml", postList];
        }
        private dynamic getAPost(dynamic parameters)
        {
            Post item = _post.Get(parameters.id);
            if(item != null)
            {
              return View["Views/Pages/Edit.cshtml", item];
            }
            else
            {
                return HttpStatusCode.NotFound;
            }
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