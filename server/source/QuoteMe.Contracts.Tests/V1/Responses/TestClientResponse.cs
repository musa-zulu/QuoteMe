using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using QuoteMe.Contracts.V1.Responses;
using System;

namespace QuoteMe.Contracts.Tests.V1.Responses
{
    [TestFixture]
    public class TestClientResponse
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new ClientResponse());
            //---------------Test Result -----------------------
        }

        [TestCase("ClientID", typeof(Guid))]
        [TestCase("Title", typeof(string))]
        [TestCase("FirstName", typeof(string))]
        [TestCase("MiddleName", typeof(string))]
        [TestCase("LastName", typeof(string))]
        [TestCase("Suffix", typeof(string))]
        [TestCase("Email", typeof(string))]
        [TestCase("EmailPromotion", typeof(bool))]
        [TestCase("AdditionalContactInfo", typeof(string))]
        [TestCase("Demographics", typeof(string))]
        [TestCase("PersonTypeID", typeof(Guid))]
        [TestCase("AddressID", typeof(Guid))]
        [TestCase("PhoneID", typeof(Guid))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(ClientResponse);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);

            //---------------Test Result -----------------------
        }
    }
}
