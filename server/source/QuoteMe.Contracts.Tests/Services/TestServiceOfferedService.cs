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
    public class TestServiceOfferedService
    {
        private ApplicationDbContext _dbContext;
        private IServiceOfferedService _serviceOfferedService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase("testdb")
                                    .Options;
            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();

            _serviceOfferedService = new ServiceOfferedService(_dbContext);
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
            Assert.DoesNotThrow(() => new ServiceOfferedService(Substitute.For<IApplicationDbContext>()));
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenApplicationDbContextIsNull_ShouldThrow()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ServiceOfferedService(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("applicationDbContext", ex.ParamName);
        }

        [Test]
        public void GetServiceOffered_GivenNoServiceOfferedExist_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _serviceOfferedService.GetServicesOffered();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetServicesOffered_GivenServiceOfferedExistInRepo_ShouldReturnListOfServiceOffered()
        {
            //---------------Set up test pack-------------------           
            SeedDb(1);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _serviceOfferedService.GetServicesOffered();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CreateServiceOffered_GivenServiceOfferedIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _serviceOfferedService.CreateServiceOffered(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("servicesOffered", ex.ParamName);
        }

        [Test]
        public void CreateServiceOffered_GivenServiceOffered_ShouldSaveServiceOffered()
        {
            //---------------Set up test pack-------------------
            var serviceOffered = ServiceOfferedBuilder.BuildRandom();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _serviceOfferedService.CreateServiceOffered(serviceOffered);
            //---------------Test Result -----------------------
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CreateServiceOffered_GivenValidServiceOffered_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var serviceOffered = ServiceOfferedBuilder.BuildRandom();
            var dbContext = Substitute.For<IApplicationDbContext>();
            _serviceOfferedService = new ServiceOfferedService(dbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _serviceOfferedService.CreateServiceOffered(serviceOffered);
            //---------------Test Result -----------------------
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void GetServiceOfferedById_GivenIdIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _serviceOfferedService.GetServiceOfferedById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("serviceOfferedId", ex.ParamName);
        }

        [Test]
        public void GetServiceOfferedById_GivenValidId_ShouldReturnServiceOfferedWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var serviceOffered = SeedDb(1).FirstOrDefault();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            if (serviceOffered == null) return;
            var result = _serviceOfferedService.GetServiceOfferedById(serviceOffered.ServicesOfferedId);
            //---------------Test Result -----------------------
            Assert.AreEqual(serviceOffered, result);
        }

        [Test]
        public void DeleteServiceOffered_GivenEmptyServiceOffered_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _serviceOfferedService.DeleteServiceOffered(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("servicesOffered", ex.ParamName);
        }

        [Test]
        public void DeleteServiceOffered_GivenValidServiceOffered_ShouldDeleteServiceOffered()
        {
            //---------------Set up test pack-------------------
            var serviceOffered = SeedDb(1).FirstOrDefault();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _serviceOfferedService.DeleteServiceOffered(serviceOffered);
            //---------------Test Result -----------------------
            var servicesOfferedFromRepo = _serviceOfferedService.GetServicesOffered();
            CollectionAssert.DoesNotContain(servicesOfferedFromRepo, serviceOffered);
        }

        [Test]
        public void DeleteServiceOffered_GivenValidServiceOffered_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var serviceOffered = SeedDb(1).FirstOrDefault();
            var dbContext = Substitute.For<IApplicationDbContext>();
            _serviceOfferedService = new ServiceOfferedService(dbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _serviceOfferedService.DeleteServiceOffered(serviceOffered);
            //---------------Test Result -----------------------
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void UpdateServiceOffered_GivenInvalidExistingServiceOffered_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => _serviceOfferedService.UpdateServiceOffered(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("serviceOfferedToUpdate", ex.ParamName);
        }

        private List<ServicesOffered> SeedDb(int servicesCount)
        {
            var servicesOffered = new List<ServicesOffered>();
            for (var c = 1; c <= servicesCount; c++)
            {
                var serviceOffered = new ServiceOfferedBuilder().WithRandomProps().Build();
                servicesOffered.Add(serviceOffered);
            }

            foreach (var serviceOffered in servicesOffered)
                _serviceOfferedService.CreateServiceOffered(serviceOffered);

            return servicesOffered;
        }
    }
}