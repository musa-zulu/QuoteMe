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
    public class TestServiceTypesOfferedService
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition---------------

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
            var applicationDbContext = CreateApplicationDbContext();
            var serviceTypeService = CreateServiceTypeService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = serviceTypeService.GetServiceTypes();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetServiceTypes_GivenServiceTypesExistInRepo_ShouldReturnListOfServiceTypes()
        {
            //---------------Set up test pack-------------------           
            var serviceTypes = new List<ServiceType>();
            var dbSet = DbSetSupport<ServiceType>.CreateDbSetWithAddRemoveSupport(serviceTypes);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var serviceTypeService = CreateServiceTypeService(applicationDbContext);

            var serviceType = ServiceTypeBuilder.BuildRandom();
            serviceTypes.Add(serviceType);
            dbSet.GetEnumerator().Returns(_ => serviceTypes.GetEnumerator());
            applicationDbContext.ServiceTypes.Returns(_ => dbSet);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = serviceTypeService.GetServiceTypes();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CreateServiceType_GivenServiceTypeIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var serviceTypeService = CreateServiceTypeService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                serviceTypeService.CreateServiceType(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("serviceType", ex.ParamName);
        }

        [Test]
        public void CreateServiceType_GivenServiceType_ShouldSaveServiceType()
        {
            //---------------Set up test pack-------------------
            var serviceType = ServiceTypeBuilder.BuildRandom();
            var serviceTypes = new List<ServiceType>();

            var dbSet = DbSetSupport<ServiceType>.CreateDbSetWithAddRemoveSupport(serviceTypes);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var serviceTypeService = CreateServiceTypeService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            serviceTypeService.CreateServiceType(serviceType);
            //---------------Test Result -----------------------
            var serviceTypesFromRepo = serviceTypeService.GetServiceTypes();
            CollectionAssert.Contains(serviceTypesFromRepo, serviceType);
        }

        [Test]
        public void CreateServiceType_GivenValidServiceType_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var serviceType = ServiceTypeBuilder.BuildRandom();
            var serviceTypes = new List<ServiceType>();
            var dbSet = DbSetSupport<ServiceType>.CreateDbSetWithAddRemoveSupport(serviceTypes);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var serviceTypeService = CreateServiceTypeService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            serviceTypeService.CreateServiceType(serviceType);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void GetServiceTypeById_GivenIdIsNull_ShouldThrowExcption()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var serviceTypeService = CreateServiceTypeService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                serviceTypeService.GetServiceTypeById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("serviceTypeID", ex.ParamName);
        }

        [Test]
        public void GetServiceTypeById_GivenValidId_ShoulReturnServiceTypeWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var serviceType = new ServiceTypeBuilder().WithRandomProps().Build();
            var dbSet = new FakeDbSet<ServiceType> { serviceType };
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var serviceTypeService = CreateServiceTypeService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = serviceTypeService.GetServiceTypeById(serviceType.ServiceTypeID);
            //---------------Test Result -----------------------
            Assert.AreEqual(serviceType, result);
        }

        [Test]
        public void DeleteServiceType_GivenEmptyServiceType_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var serviceTypeService = CreateServiceTypeService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                serviceTypeService.DeleteServiceType(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("serviceType", ex.ParamName);
        }

        [Test]
        public void DeleteServiceType_GivenValidServiceType_ShouldDeleteServiceType()
        {
            //---------------Set up test pack-------------------
            var serviceTypes = new List<ServiceType>();
            var serviceType = ServiceTypeBuilder.BuildRandom();

            var dbSet = DbSetSupport<ServiceType>.CreateDbSetWithAddRemoveSupport(serviceTypes);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var serviceTypeService = CreateServiceTypeService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            serviceTypeService.DeleteServiceType(serviceType);
            //---------------Test Result -----------------------
            var serviceTypesFromRepo = serviceTypeService.GetServiceTypes();
            CollectionAssert.DoesNotContain(serviceTypesFromRepo, serviceType);
        }

        [Test]
        public void DeleteServiceType_GivenValidServiceType_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var serviceTypes = new List<ServiceType>();
            var serviceType = ServiceTypeBuilder.BuildRandom();

            var dbSet = DbSetSupport<ServiceType>.CreateDbSetWithAddRemoveSupport(serviceTypes);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var serviceTypeService = CreateServiceTypeService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            serviceTypeService.DeleteServiceType(serviceType);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void UpdateServiceType_GivenInvalidExistingServiceType_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var serviceTypeService = CreateServiceTypeService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => serviceTypeService.UpdateServiceType(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("serviceTypeToUpdate", ex.ParamName);
        }

        private static ServiceTypesOfferedService CreateServiceTypeService(IApplicationDbContext applicationDbContext)
        {
            return new ServiceTypesOfferedService(applicationDbContext);
        }

        private static IApplicationDbContext CreateApplicationDbContext(IDbSet<ServiceType> dbSet = null)
        {
            if (dbSet == null) dbSet = DbSetSupport<ServiceType>.CreateDbSetWithAddRemoveSupport(new List<ServiceType>());
            var applicationDbContext = Substitute.For<IApplicationDbContext>();
            applicationDbContext.ServiceTypes.Returns(_ => dbSet);
            return applicationDbContext;
        }

    }
}
