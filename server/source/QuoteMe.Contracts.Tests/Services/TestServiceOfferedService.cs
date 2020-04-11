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
    public class TestServiceOfferedService
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition---------------

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
            var applicationDbContext = CreateApplicationDbContext();
            var serviceOfferedService = CreateServiceOfferedService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = serviceOfferedService.GetServicesOffered();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetServicesOffered_GivenServiceOfferedExistInRepo_ShouldReturnListOfServiceOffered()
        {
            //---------------Set up test pack-------------------           
            var servicesOffered = new List<ServicesOffered>();
            var dbSet = DbSetSupport<ServicesOffered>.CreateDbSetWithAddRemoveSupport(servicesOffered);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var serviceOfferedService = CreateServiceOfferedService(applicationDbContext);

            var serviceOffered = ServiceOfferedBuilder.BuildRandom();
            servicesOffered.Add(serviceOffered);
            dbSet.GetEnumerator().Returns(_ => servicesOffered.GetEnumerator());
            applicationDbContext.ServicesOffered.Returns(_ => dbSet);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = serviceOfferedService.GetServicesOffered();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CreateServiceOffered_GivenServiceOfferedIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var serviceOfferedService = CreateServiceOfferedService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                serviceOfferedService.CreateServiceOffered(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("servicesOffered", ex.ParamName);
        }

        [Test]
        public void CreateServiceOffered_GivenServiceOffered_ShouldSaveServiceOffered()
        {
            //---------------Set up test pack-------------------
            var serviceOffered = ServiceOfferedBuilder.BuildRandom();
            var servicesOffered = new List<ServicesOffered>();

            var dbSet = DbSetSupport<ServicesOffered>.CreateDbSetWithAddRemoveSupport(servicesOffered);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var serviceOfferedService = CreateServiceOfferedService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            serviceOfferedService.CreateServiceOffered(serviceOffered);
            //---------------Test Result -----------------------
            var servicesOfferedFromRepo = serviceOfferedService.GetServicesOffered();
            CollectionAssert.Contains(servicesOfferedFromRepo, serviceOffered);
        }

        [Test]
        public void CreateServiceOffered_GivenValidServiceOffered_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var serviceOffered = ServiceOfferedBuilder.BuildRandom();
            var servicesOffered = new List<ServicesOffered>();
            var dbSet = DbSetSupport<ServicesOffered>.CreateDbSetWithAddRemoveSupport(servicesOffered);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var serviceOfferedService = CreateServiceOfferedService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            serviceOfferedService.CreateServiceOffered(serviceOffered);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void GetServiceOfferedById_GivenIdIsNull_ShouldThrowExcption()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var serviceOfferedService = CreateServiceOfferedService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                serviceOfferedService.GetServiceOfferedById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("serviceOfferedID", ex.ParamName);
        }

        [Test]
        public void GetServiceOfferedById_GivenValidId_ShoulReturnServiceOfferedWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var serviceOffered = new ServiceOfferedBuilder().WithRandomProps().Build();
            var dbSet = new FakeDbSet<ServicesOffered> { serviceOffered };
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var serviceOfferedService = CreateServiceOfferedService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = serviceOfferedService.GetServiceOfferedById(serviceOffered.ServicesOfferedID);
            //---------------Test Result -----------------------
            Assert.AreEqual(serviceOffered, result);
        }

        [Test]
        public void DeleteServiceOffered_GivenEmptyServiceOffered_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var serviceOfferedService = CreateServiceOfferedService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                serviceOfferedService.DeleteServiceOffered(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("servicesOffered", ex.ParamName);
        }

        [Test]
        public void DeleteServiceOffered_GivenValidServiceOffered_ShouldDeleteServiceOffered()
        {
            //---------------Set up test pack-------------------
            var servicesOffered = new List<ServicesOffered>();
            var serviceOffered = ServiceOfferedBuilder.BuildRandom();

            var dbSet = DbSetSupport<ServicesOffered>.CreateDbSetWithAddRemoveSupport(servicesOffered);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var serviceOfferedService = CreateServiceOfferedService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            serviceOfferedService.DeleteServiceOffered(serviceOffered);
            //---------------Test Result -----------------------
            var servicesOfferedFromRepo = serviceOfferedService.GetServicesOffered();
            CollectionAssert.DoesNotContain(servicesOfferedFromRepo, serviceOffered);
        }

        [Test]
        public void DeleteServiceOffered_GivenValidServiceOffered_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var servicesOffered = new List<ServicesOffered>();
            var serviceOffered = ServiceOfferedBuilder.BuildRandom();

            var dbSet = DbSetSupport<ServicesOffered>.CreateDbSetWithAddRemoveSupport(servicesOffered);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var serviceOfferedService = CreateServiceOfferedService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            serviceOfferedService.DeleteServiceOffered(serviceOffered);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void UpdateServiceOffered_GivenInvalidExistingServiceOffered_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var serviceOfferedService = CreateServiceOfferedService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => serviceOfferedService.UpdateServiceOffered(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("serviceOfferedToUpdate", ex.ParamName);
        }

        private static ServiceOfferedService CreateServiceOfferedService(IApplicationDbContext applicationDbContext)
        {
            return new ServiceOfferedService(applicationDbContext);
        }

        private static IApplicationDbContext CreateApplicationDbContext(IDbSet<ServicesOffered> dbSet = null)
        {
            if (dbSet == null) dbSet = DbSetSupport<ServicesOffered>.CreateDbSetWithAddRemoveSupport(new List<ServicesOffered>());
            var applicationDbContext = Substitute.For<IApplicationDbContext>();
            applicationDbContext.ServicesOffered.Returns(_ => dbSet);
            return applicationDbContext;
        }
    }
}