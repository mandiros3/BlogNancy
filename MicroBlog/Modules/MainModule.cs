﻿using System;
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

            
            //Todo Maybe put all routes into their own file/model
            Get["/"] = Home;
            Get["/write"] = Write;
            Get["/login"] = Login;

        }

        //Actions Methods here: So the contructor doesn't get bloated.


        //Nancy will look for a razor file with  a file name that matches the class name of the viewmodel.
        private dynamic Home(dynamic o)
        {
            return View["Views/Pages/Home"];
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