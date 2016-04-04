using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace BlogNancy.Modules
{
    /// <summary>
    /// Handles the routes for the user profile (admin)
    /// </summary>
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