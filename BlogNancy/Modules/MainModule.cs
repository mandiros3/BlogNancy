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
           
            //The main routes, each route will call a method, this will keep the constructor lean.
            Get["/"] = Home;
            //display the form to input data
            Get["/posts/new"] = createPostForm;
            Post["/posts/new"] = createPost;
            Get["/posts/{id:int}"] = getById;
            Delete["/posts/{id:int}"] = deletePost;

            //Todo: Turn this route into a function like the others.
            Post["/posts/{id:int}", true] = async (parameters, ctx) =>
            {
                //this.needs authentication
                var updatedPost = this.Bind<Post>();
                await _post.Update(updatedPost);
                return Response.AsRedirect("/");
            };
        }

        //Action Methods definitions (Called from the contructor)
      
        public dynamic Home(dynamic parameters)
        {
            var postList = _post.GetAll();
            return View["Views/Pages/Home.cshtml", postList];
            //return "Hello World";
        }
       
        public dynamic getById(dynamic parameters)
        {
            Post item = _post.Get(parameters.id);
            if (item == null)
            {
                return HttpStatusCode.NotFound;
            }
            return View["Views/Pages/Edit.cshtml", item];
        }

        public dynamic createPostForm(dynamic parameters)
        {
            var post = new Post();
            return View["Views/Pages/Write", post];
        }

        public dynamic createPost(dynamic parameters)
        {
            //Binds model to view
            var post = this.Bind<Post>();
            if (string.IsNullOrEmpty(post.Title) || string.IsNullOrEmpty(post.Content))
            {
                //Need to return a HTTP status code in later versions
                return ("<h2>Cannot submit empty form</h2> <a href='/posts/new'>Go back...</a>");
            }
            _post.Create(post);
            return Response.AsRedirect("/");
        }

        public dynamic deletePost(dynamic parameters)
        {
            int _id = parameters.id;
            var result = _post.Delete(_id);
            return result == false ? HttpStatusCode.NotFound : Response.AsRedirect("/");
        }
    
    }
}