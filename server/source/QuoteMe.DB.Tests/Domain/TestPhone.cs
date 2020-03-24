using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using QuoteMe.DB.Domain;
using System;

namespace QuoteMe.DB.Tests.Domain
{
    [TestFixture]
    public class TestPhone
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new Phone());
            //---------------Test Result -----------------------
        }

        [TestCase("PhoneID", typeof(Guid))]
        [TestCase("PhoneNumber", typeof(long))]
        [TestCase("PhoneTypeID", typeof(Guid))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(Phone);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);

            //---------------Test Result -----------------------
        }
    }
}