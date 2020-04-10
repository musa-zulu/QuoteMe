using NUnit.Framework;
using QuoteMe.Contracts.Services;

namespace QuoteMe.Contracts.Tests.Services
{
    [TestFixture]
    public class TestUriService
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition---------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new UriService(string.Empty));
            //---------------Test Result -----------------------
        }

        [Test]
        public void GetAllUri_ShouldReturnUri()
        {
            //---------------Set up test pack-------------------
            const string baseUri = "http://localhost/";
            var uriService = new UriService(baseUri);
            //---------------Assert Precondition---------------

            //---------------Execute Test ----------------------
            var result = uriService.GetAllUri();
            //---------------Test Result -----------------------
            Assert.AreEqual(baseUri, result.AbsoluteUri);

        }
    }
}
