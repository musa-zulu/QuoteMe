using NSubstitute;
using NUnit.Framework;
using QuoteMe.Contracts.Services;
using QuoteMe.DB;
using QuoteMe.DB.Domain;
using QuoteMe.Tests.Common.Builders.Models;
using QuoteMe.Tests.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace QuoteMe.Contracts.Tests.Services
{
    [TestFixture]
    public class TestBusinessService
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition---------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new BusinessService(Substitute.For<IApplicationDbContext>()));
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenApplicationDbContextIsNull_ShouldThrow()
        {

            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new BusinessService(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("applicationDbContext", ex.ParamName);
        }

        [Test]
        public void GetBusinesses_GivenNoBusinessExist_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var businessService = CreateBusinessService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = businessService.GetBusinesses();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetBusinesses_GivenBusinessExistInRepo_ShouldReturnListOfBusiness()
        {
            //---------------Set up test pack-------------------           
            var businesses = new List<Business>();
            var dbSet = CreateDbSetWithAddRemoveSupport(businesses);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var businessService = CreateBusinessService(applicationDbContext);

            var business = BusinessBuilder.BuildRandom();
            businesses.Add(business);
            dbSet.GetEnumerator().Returns(_ => businesses.GetEnumerator());
            applicationDbContext.Businesses.Returns(_ => dbSet);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = businessService.GetBusinesses();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CreateBusiness_GivenBusinessIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var businessService = CreateBusinessService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                businessService.CreateBusiness(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("business", ex.ParamName);
        }

        [Test]
        public void CreateBusiness_GivenBusiness_ShouldSaveBusiness()
        {
            //---------------Set up test pack-------------------
            var business = BusinessBuilder.BuildRandom();
            var businesses = new List<Business>();

            var dbSet = CreateDbSetWithAddRemoveSupport(businesses);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var businesService = CreateBusinessService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            businesService.CreateBusiness(business);
            //---------------Test Result -----------------------
            var businessFromRepo = businesService.GetBusinesses();
            CollectionAssert.Contains(businessFromRepo, business);
        }

        [Test]
        public void CreateBusiness_GivenValidBusiness_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var business = BusinessBuilder.BuildRandom();
            var businesses = new List<Business>();
            var dbSet = CreateDbSetWithAddRemoveSupport(businesses);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var businesService = CreateBusinessService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            businesService.CreateBusiness(business);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void GetBusinessById_GivenIdIsNull_ShouldThrowExcption()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var businesService = CreateBusinessService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                businesService.GetBusinessById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("businessID", ex.ParamName);
        }

        [Test]
        public void GetBusinessById_GivenValidId_ShoulReturnBusinessWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var business = new BusinessBuilder().WithRandomProps().Build();
            var dbSet = new FakeDbSet<Business> { business };
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var businesService = CreateBusinessService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = businesService.GetBusinessById(business.BusinessID);
            //---------------Test Result -----------------------
            Assert.AreEqual(business, result);
        }

        [Test]
        public void DeleteBusiness_GivenNullBusiness_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var businesService = CreateBusinessService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                businesService.DeleteBusiness(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("business", ex.ParamName);
        }

        [Test]
        public void DeleteBusiness_GivenValidBusiness_ShouldDeleteBussiness()
        {
            //---------------Set up test pack-------------------
            var businesses = new List<Business>();
            var business = BusinessBuilder.BuildRandom();

            var dbSet = CreateDbSetWithAddRemoveSupport(businesses);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var businesService = CreateBusinessService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            businesService.DeleteBusiness(business);
            //---------------Test Result -----------------------
            var businesesFromRepo = businesService.GetBusinesses();
            CollectionAssert.DoesNotContain(businesesFromRepo, business);
        }

        [Test]
        public void DeleteBusiness_GivenValidBusiness_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var businesses = new List<Business>();
            var business = BusinessBuilder.BuildRandom();

            var dbSet = CreateDbSetWithAddRemoveSupport(businesses);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var businesService = CreateBusinessService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            businesService.DeleteBusiness(business);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void UpdateBusiness_GivenInvalidExistingBusiness_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var businesService = CreateBusinessService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => businesService.UpdateBusiness(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("businessToUpdate", ex.ParamName);
        }


        private static BusinessService CreateBusinessService(IApplicationDbContext applicationDbContext)
        {
            return new BusinessService(applicationDbContext);
        }

        private static IDbSet<Business> CreateDbSetWithAddRemoveSupport(List<Business> businesses)
        {
            var dbSet = Substitute.For<IDbSet<Business>>();

            dbSet.Add(Arg.Any<Business>()).Returns(info =>
            {
                businesses.Add(info.ArgAt<Business>(0));
                return info.ArgAt<Business>(0);
            });

            dbSet.Remove(Arg.Any<Business>()).Returns(info =>
            {
                businesses.Remove(info.ArgAt<Business>(0));
                return info.ArgAt<Business>(0);
            });

            dbSet.GetEnumerator().Returns(_ => businesses.GetEnumerator());
            return dbSet;
        }

        private static IApplicationDbContext CreateApplicationDbContext(IDbSet<Business> dbSet = null)
        {
            if (dbSet == null) dbSet = CreateDbSetWithAddRemoveSupport(new List<Business>());
            var applicationDbContext = Substitute.For<IApplicationDbContext>();
            applicationDbContext.Businesses.Returns(_ => dbSet);
            return applicationDbContext;
        }
    }
}
