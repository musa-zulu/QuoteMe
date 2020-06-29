using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using QuoteMe.Contracts.V1.Responses;
using System;

namespace QuoteMe.Contracts.Tests.V1.Responses
{
    [TestFixture]
    public class TestResponse
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new Response<Type>());
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenAnInput_ShouldSetDataToThatValue()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var sut = new Response<int>(1);
            //---------------Test Result -----------------------
            Assert.AreEqual(1, sut.Data);
        }

        [TestCase("Data", typeof(Type))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(Response<Type>);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}
