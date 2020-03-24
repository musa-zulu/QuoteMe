using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using QuoteMe.DB.Domain;
using System;

namespace QuoteMe.DB.Tests.Domain
{
    [TestFixture]
    public class TestServicesOffered
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new ServicesOffered());
            //---------------Test Result -----------------------
        }

        [TestCase("ServicesOfferedID", typeof(Guid))]
        [TestCase("Description", typeof(string))]
        [TestCase("ServiceTypeID", typeof(Guid))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(ServicesOffered);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);

            //---------------Test Result -----------------------
        }
    }
}