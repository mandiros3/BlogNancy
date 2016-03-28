using NUnit.Framework;
using Nancy.Diagnostics.Modules;
using Nancy.Testing;
using Nancy;
namespace MicroBlog.Test
{
    [TestFixture]
    public class MainModuleTest
    {
        private Browser _browser;
        [SetUp]
        public void SetUp()
        {
            /* I had to tell Nancy which modules to load. 		
           You can do this by using the configurable bootstrapper, 		
            which gives you an API to configure parts of Nancy yourself. 		
          */

            //Given		
            var bootstrapper = new ConfigurableBootstrapper(with =>
             {
                 with.Module<MainModule>();
             });
            _browser = new Browser(bootstrapper);
            bootstrapper.Initialise();
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
        public void should_return_400_when_empty_input()
        {
            var result = _browser.Post("/posts/new", with =>
            {
                with.HttpRequest();
                with.Header("content-type", "text/html");
                with.Query("title", null);
                with.Query("content", null);
            });
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }


    }
}
