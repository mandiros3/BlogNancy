using System;
using System.Web;
using Owin;
using System.IO;

namespace MicroBlog
{
    public class Startup
    {
            public static string dbSource
        {
           //get { return Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "microblog.sqlite"; }
            get { return @"C:\db\microblog.sqlite"; }

            private set { }
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();

        }
    }
}