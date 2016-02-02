using System;
using System.Web;
using Owin;

namespace MicroBlog
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}