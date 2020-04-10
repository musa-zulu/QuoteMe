using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using QuoteMe.DB.Domain;
using System;

namespace QuoteMe.DB.Tests.Domain
{
    [TestFixture]
    public class TestClient
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new Client());
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
            var sut = typeof(Client);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);

            //---------------Test Result -----------------------
        }
    }
}