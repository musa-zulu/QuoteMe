using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using QuoteMe.Contracts.V1.Requests.Queries;
using System;

namespace QuoteMe.Contracts.Tests.V1.Requests.Queries
{
    [TestFixture]
    public class TestPaginationQuery
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new PaginationQuery());
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenNoParameters_ShouldSetPageNumberToOne()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var sut = new PaginationQuery();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, sut.PageNumber);
        }

        [Test]
        public void Construct_GivenNoParameters_ShouldSetPageSizeTo100()
        {

            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var sut = new PaginationQuery();
            //---------------Test Result -----------------------
            Assert.AreEqual(100, sut.PageSize);
        }

        [Test]
        public void Construct_GivenPageNumber_ShouldSetPageNumberToGivenValue()
        {
            //---------------Set up test pack-------------------
            var pageNumber = 2;
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var sut = new PaginationQuery(pageNumber, 0);
            //---------------Test Result -----------------------
            Assert.AreEqual(pageNumber, sut.PageNumber);
        }


        [Test]
        public void Construct_GivenPageSize_ShouldSetPageSizeToGivenValue()
        {
            //---------------Set up test pack-------------------
            var pageSize = 12;
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var sut = new PaginationQuery(0, pageSize);
            //---------------Test Result -----------------------
            Assert.AreEqual(pageSize, sut.PageSize);
        }

        [TestCase("PageNumber", typeof(int))]
        [TestCase("PageSize", typeof(int))]
        public void Type_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(PaginationQuery);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}