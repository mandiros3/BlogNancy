﻿using System;
using System.Web;
using Owin;
using System.IO;

namespace BlogNancy
{
    public class Startup
    {
            public static string dbSource
        {
           //get { return Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "microblog.sqlite"; }
            //get { return @"C:\db\microblog.sqlite"; }
        get{ return @"|DataDirectory|microblog.sqlite"; }
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();

        }
    }
}