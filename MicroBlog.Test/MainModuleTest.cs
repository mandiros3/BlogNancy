using System.Runtime.CompilerServices;
using Nancy;
using Nancy.Diagnostics.Modules;
using NUnit.Framework;
using Nancy.Testing;

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
            var result = _browser.Get("/", with => {
            with.HttpRequest();
            });
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

      


    }
}
