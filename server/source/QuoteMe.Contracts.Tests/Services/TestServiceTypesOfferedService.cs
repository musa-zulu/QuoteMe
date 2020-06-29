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
    public class TestServiceTypesOfferedService
    {
        private ApplicationDbContext _dbContext;
        private IServiceTypesOfferedService _serviceTypesOfferedService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase("testdb")
                                    .Options;
            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();

            _serviceTypesOfferedService = new ServiceTypesOfferedService(_dbContext);
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
            Assert.DoesNotThrow(() => new ServiceTypesOfferedService(Substitute.For<IApplicationDbContext>()));
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenApplicationDbContextIsNull_ShouldThrow()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ServiceTypesOfferedService(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("applicationDbContext", ex.ParamName);
        }

        [Test]
        public void GetServiceTypes_GivenNoServiceTypeExist_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _serviceTypesOfferedService.GetServiceTypes();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetServiceTypes_GivenServiceTypesExistInRepo_ShouldReturnListOfServiceTypes()
        {
            //---------------Set up test pack-------------------           
            SeedDb(1);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _serviceTypesOfferedService.GetServiceTypes();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CreateServiceType_GivenServiceTypeIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _serviceTypesOfferedService.CreateServiceType(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("serviceType", ex.ParamName);
        }

        [Test]
        public void CreateServiceType_GivenServiceType_ShouldSaveServiceType()
        {
            //---------------Set up test pack-------------------
            var serviceType = ServiceTypeBuilder.BuildRandom();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _serviceTypesOfferedService.CreateServiceType(serviceType);
            //---------------Test Result -----------------------
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CreateServiceType_GivenValidServiceType_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var serviceType = ServiceTypeBuilder.BuildRandom();
            var dbContext = Substitute.For<IApplicationDbContext>();
            _serviceTypesOfferedService = new ServiceTypesOfferedService(dbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _serviceTypesOfferedService.CreateServiceType(serviceType);
            //---------------Test Result -----------------------
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void GetServiceTypeById_GivenIdIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _serviceTypesOfferedService.GetServiceTypeById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("serviceTypeID", ex.ParamName);
        }

        [Test]
        public void GetServiceTypeById_GivenValidId_ShouldReturnServiceTypeWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var serviceType = SeedDb(1).FirstOrDefault();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            if (serviceType == null) return;
            var result = _serviceTypesOfferedService.GetServiceTypeById(serviceType.ServiceTypeId);
            //---------------Test Result -----------------------
            Assert.AreEqual(serviceType, result);
        }

        [Test]
        public void DeleteServiceType_GivenEmptyServiceType_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _serviceTypesOfferedService.DeleteServiceType(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("serviceType", ex.ParamName);
        }

        [Test]
        public void DeleteServiceType_GivenValidServiceType_ShouldDeleteServiceType()
        {
            //---------------Set up test pack-------------------
            var serviceType = SeedDb(1).FirstOrDefault();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _serviceTypesOfferedService.DeleteServiceType(serviceType);
            //---------------Test Result -----------------------
            var serviceTypesFromRepo = _serviceTypesOfferedService.GetServiceTypes();
            CollectionAssert.DoesNotContain(serviceTypesFromRepo, serviceType);
        }

        [Test]
        public void DeleteServiceType_GivenValidServiceType_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var serviceType = ServiceTypeBuilder.BuildRandom();
            var dbContext = Substitute.For<IApplicationDbContext>();
            _serviceTypesOfferedService = new ServiceTypesOfferedService(dbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _serviceTypesOfferedService.DeleteServiceType(serviceType);
            //---------------Test Result -----------------------
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void UpdateServiceType_GivenInvalidExistingServiceType_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => _serviceTypesOfferedService.UpdateServiceType(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("serviceTypeToUpdate", ex.ParamName);
        }

        private List<ServiceType> SeedDb(int serviceCount)
        {
            var serviceTypes = new List<ServiceType>();
            for (var c = 1; c <= serviceCount; c++)
            {
                var serviceType = new ServiceTypeBuilder().WithRandomProps().Build();
                serviceTypes.Add(serviceType);
            }

            foreach (var serviceType in serviceTypes)
                _serviceTypesOfferedService.CreateServiceType(serviceType);

            return serviceTypes;
        }

    }
}
