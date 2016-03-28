using System;
using System.Collections.Generic;
using BlogNancy.Models;
using Nancy;
using Nancy.ModelBinding;
using BlogNancy.Interfaces;
using BlogNancy.DataProviders;


namespace BlogNancy.Modules
{
    public class MainModule : NancyModule
    {

        IRepository _post = new DataBaseRepository();

        public MainModule()
        {  //Alpha version: 1. 
              // add a basepath to avoid repetion, but make index do something else, separate module   
            Get["/"] = Home;
            Get["/posts/new"] = newPost_GET;
            Post["/posts/new"] = newPost_POST;
            Get["/posts/{id:int}"] = getAPost;
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
            var postList = _post.GetAll();
             return View["Views/Pages/Home.cshtml", postList];
            //return "Hello World";
        }

        private dynamic getAPost(dynamic parameters)
        {
            Post item = _post.Get(parameters.id);
            if(item == null)
            {
                return HttpStatusCode.NotFound;
            }
            return View["Views/Pages/Edit.cshtml", item];    
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
            if (string.IsNullOrEmpty(post.Title) || string.IsNullOrEmpty(post.Content))
            {
                return HttpStatusCode.BadRequest;
            }
                _post.Create(post);
                return Response.AsRedirect("/");         
        }

        public dynamic Remove(dynamic parameters)
        {
            int _id = parameters.id;
            var result = _post.Delete(_id);
            return result == false ? HttpStatusCode.NotFound : Response.AsRedirect("/");
        }

       

    }
}