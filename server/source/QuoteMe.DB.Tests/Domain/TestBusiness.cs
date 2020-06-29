using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;

namespace QuoteMe.DB.Tests.Domain
{
    [TestFixture]
    public class TestBusiness
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new Business());
            //---------------Test Result -----------------------
        }

        [TestCase("BusinessId", typeof(Guid))]
        [TestCase("Name", typeof(string))]
        [TestCase("Demographics", typeof(string))]
        [TestCase("Email", typeof(string))]
        [TestCase("Description", typeof(string))]
        [TestCase("PhoneNumbers", typeof(List<Phone>))]
        [TestCase("Addresses", typeof(List<Address>))]
        [TestCase("ServicesOffered", typeof(List<ServicesOffered>))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(Business);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);

            //---------------Test Result -----------------------
        }
    }
}