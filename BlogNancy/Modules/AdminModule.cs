using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace MicroBlog.Modules
{
    public class AdminModule : NancyModule
    {
        public AdminModule()
        {
            Get["/user/login"] = Login;
        }
        private dynamic Login(dynamic parameters)
        {
            return View["Views/Pages/Login"];
        }


    }
}