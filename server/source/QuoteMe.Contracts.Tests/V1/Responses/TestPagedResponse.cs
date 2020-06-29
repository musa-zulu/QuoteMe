using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using QuoteMe.Contracts.V1.Responses;
using System;
using System.Collections.Generic;

namespace QuoteMe.Contracts.Tests.V1.Responses
{
    [TestFixture]
    public class TestPagedResponse
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new PagedResponse<Type>());
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenInput_ShouldSetData()
        {
            //---------------Set up test pack-------------------
            IEnumerable<int> data = new int[] { 1 };
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var sut = new PagedResponse<int>(data);
            //---------------Test Result -----------------------      
            foreach (var val in sut.Data)
                Assert.AreEqual(1, val);
        }

        [TestCase("Data", typeof(IEnumerable<Type>))]
        [TestCase("PageNumber", typeof(int?))]
        [TestCase("PageSize", typeof(int?))]
        [TestCase("NextPage", typeof(string))]
        [TestCase("PreviousPage", typeof(string))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(PagedResponse<Type>);

            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}
