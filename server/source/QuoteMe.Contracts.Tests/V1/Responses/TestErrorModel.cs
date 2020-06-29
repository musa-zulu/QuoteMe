using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using QuoteMe.Contracts.V1.Responses;
using System;

namespace QuoteMe.Contracts.Tests.V1.Responses
{
    [TestFixture]
    public class TestErrorModel
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new ErrorModel());
            //---------------Test Result -----------------------
        }

        [TestCase("FieldName", typeof(string))]
        [TestCase("Message", typeof(string))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(ErrorModel);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}
