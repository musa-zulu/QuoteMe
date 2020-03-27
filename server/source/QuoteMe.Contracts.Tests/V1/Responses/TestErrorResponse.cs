using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using QuoteMe.Contracts.V1.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuoteMe.Contracts.Tests.V1.Responses
{
    [TestFixture]
    public class TestErrorResponse
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new ErrorResponse());
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenError_ShouldAddToErrorsCollection()
        {
            //---------------Set up test pack-------------------
            var error = new ErrorModel
            {
                FieldName = "field",
                Message = "this is an error"
            };
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var sut = new ErrorResponse(error);
            //---------------Test Result -----------------------
            var errorFromTheList = sut.Errors.FirstOrDefault();
            Assert.AreEqual(1, sut.Errors.Count());
            Assert.AreEqual(error, errorFromTheList);
        }

        [TestCase("Errors", typeof(List<ErrorModel>))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(ErrorResponse);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}

