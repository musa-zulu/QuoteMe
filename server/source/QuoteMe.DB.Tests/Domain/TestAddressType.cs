﻿using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using QuoteMe.DB.Domain;
using System;

namespace QuoteMe.DB.Tests.Domain
{
    [TestFixture]
    public class TestAddressType
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new AddressType());
            //---------------Test Result -----------------------
        }

        [TestCase("AddressTypeId", typeof(Guid))]
        [TestCase("Name", typeof(string))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(AddressType);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);

            //---------------Test Result -----------------------
        }
    }
}