using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using QuoteMe.DB.Domain;
using System;

namespace QuoteMe.DB.Tests.Domain
{
    [TestFixture]
    public class TestAddress
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new Address());
            //---------------Test Result -----------------------
        }

        [TestCase("AddressId", typeof(Guid))]
        [TestCase("AddressLine1", typeof(string))]
        [TestCase("AddressLine2", typeof(string))]
        [TestCase("CityOrTown", typeof(string))]
        [TestCase("PostalCode", typeof(int))]
        [TestCase("SpecialDescription", typeof(string))]
        [TestCase("StateProvinceId", typeof(Guid))]
        [TestCase("AddressTypeId", typeof(Guid))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(Address);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);

            //---------------Test Result -----------------------
        }
    }
}