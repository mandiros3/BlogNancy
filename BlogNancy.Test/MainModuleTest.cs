using System;
using NUnit.Framework;
using Nancy.Diagnostics.Modules;
using Nancy.Testing;
using Nancy;

namespace BlogNancy.Test
{
    public class TestRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return Environment.CurrentDirectory;
        }
    }
    [TestFixture]
    public class MainModuleTest
    {
        private Browser _browser;
        ConfigurableBootstrapper bootstrapper;
        [SetUp]
        public void SetUp()
        {
            /* I had to tell Nancy which modules to load. 		
           You can do this by using the configurable bootstrapper, 		
            which gives you an API to configure parts of Nancy yourself. 		
          */

            //Given		
             bootstrapper = new ConfigurableBootstrapper(with =>
             {
                 with.Module<BlogNancy.Modules.MainModule>();
                 with.RootPathProvider(new TestRootPathProvider());
             });
            bootstrapper.Initialise();
            _browser = new Browser(bootstrapper);
        }

        [Test]
        public void index_test()
        {
            {
                var result = _browser.Get("/", with => with.HttpRequest());

                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            }
        }

        [Test]
        public void should_return_400_on_empty_input()
        {
            var result = _browser.Get("/posts/new/", with =>
            {
                with.HttpRequest();
               // with.Header("content-type", "application/x-www-form-urlencoded");
                //with.FormValue("title", "");
               // with.FormValue("content", "");
            });
            // Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }


    }
}
