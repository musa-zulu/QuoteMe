using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.Contracts.Services;
using QuoteMe.DB;
using QuoteMe.DB.Domain;
using QuoteMe.Tests.Common.Builders.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuoteMe.Contracts.Tests.Services
{
    [TestFixture]
    public class TestBusinessService
    {
        private ApplicationDbContext _dbContext;
        private IBusinessService _businessService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase("testdb")
                                    .Options;
            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();

            _businessService = new BusinessService(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext?.Database.EnsureDeleted();
        }

        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
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
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _businessService.GetBusinesses();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetBusinesses_GivenBusinessExistInRepo_ShouldReturnListOfBusiness()
        {
            //---------------Set up test pack-------------------           
            SeedDb(1);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _businessService.GetBusinesses();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CreateBusiness_GivenBusinessIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _businessService.CreateBusiness(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("business", ex.ParamName);
        }

        [Test]
        public void CreateBusiness_GivenBusiness_ShouldSaveBusiness()
        {
            //---------------Set up test pack-------------------
            var business = BusinessBuilder.BuildRandom();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = _businessService.CreateBusiness(business);
            //---------------Test Result -----------------------          
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CreateBusiness_GivenValidBusiness_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var business = BusinessBuilder.BuildRandom();
            var dbContext = Substitute.For<IApplicationDbContext>();
            _businessService = new BusinessService(dbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _businessService.CreateBusiness(business);
            //---------------Test Result -----------------------
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void GetBusinessById_GivenIdIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _businessService.GetBusinessById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("businessID", ex.ParamName);
        }

        [Test]
        public void GetBusinessById_GivenValidId_ShouldReturnBusinessWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var business = SeedDb(1).FirstOrDefault();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            if (business == null) return;
            var result = _businessService.GetBusinessById(business.BusinessId);
            //---------------Test Result -----------------------
            Assert.AreEqual(business, result);
        }

        [Test]
        public void DeleteBusiness_GivenNullBusiness_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _businessService.DeleteBusiness(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("business", ex.ParamName);
        }

        [Test]
        public void DeleteBusiness_GivenValidBusiness_ShouldDeleteBusiness()
        {
            //---------------Set up test pack-------------------
            var business = SeedDb(1).FirstOrDefault();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _businessService.DeleteBusiness(business);
            //---------------Test Result -----------------------
            var businessesFromRepo = _businessService.GetBusinesses();
            CollectionAssert.DoesNotContain(businessesFromRepo, business);
        }

        [Test]
        public void DeleteBusiness_GivenValidBusiness_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var business = SeedDb(1).FirstOrDefault();
            var dbContext = Substitute.For<IApplicationDbContext>();
            _businessService = new BusinessService(dbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _businessService.DeleteBusiness(business);
            //---------------Test Result -----------------------
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void UpdateBusiness_GivenInvalidExistingBusiness_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => _businessService.UpdateBusiness(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("businessToUpdate", ex.ParamName);
        }

        private List<Business> SeedDb(int businessCount)
        {
            var businesses = new List<Business>();
            for (var a = 1; a <= businessCount; a++)
            {
                var business = new BusinessBuilder().WithRandomProps().Build();
                businesses.Add(business);
            }

            foreach (var business in businesses)
                _businessService.CreateBusiness(business);

            return businesses;
        }
    }
}
