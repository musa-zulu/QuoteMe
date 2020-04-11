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
    public class TestAddressTypeService
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition---------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new AddressService(Substitute.For<IApplicationDbContext>()));
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenApplicationDbContextIsNull_ShouldThrow()
        {

            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new AddressService(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("applicationDbContext", ex.ParamName);
        }

        [Test]
        public void GetAddressTypes_GivenNoAddressTypeExist_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var addressTypeService = CreateAddressTypeService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = addressTypeService.GetAddressTypes();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetAddressTypes_GivenAddressTypesExistInRepo_ShouldReturnListOfAddressTypes()
        {
            //---------------Set up test pack-------------------           
            var addressTypes = new List<AddressType>();
            var dbSet = DbSetSupport<AddressType>.CreateDbSetWithAddRemoveSupport(addressTypes);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var addressTypeService = CreateAddressTypeService(applicationDbContext);

            var addressType = AddressTypeBuilder.BuildRandom();
            addressTypes.Add(addressType);
            dbSet.GetEnumerator().Returns(_ => addressTypes.GetEnumerator());
            applicationDbContext.AddressTypes.Returns(_ => dbSet);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = addressTypeService.GetAddressTypes();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CreateAddressType_GivenAddressTypeIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var addressTypeService = CreateAddressTypeService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                addressTypeService.CreateAddressType(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("addressType", ex.ParamName);
        }

        [Test]
        public void CreateAddressType_GivenAddressType_ShouldSaveAddressType()
        {
            //---------------Set up test pack-------------------
            var addressType = AddressTypeBuilder.BuildRandom();
            var addressTypes = new List<AddressType>();

            var dbSet = DbSetSupport<AddressType>.CreateDbSetWithAddRemoveSupport(addressTypes);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var addressTypeService = CreateAddressTypeService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            addressTypeService.CreateAddressType(addressType);
            //---------------Test Result -----------------------
            var addressTypesFromRepo = addressTypeService.GetAddressTypes();
            CollectionAssert.Contains(addressTypesFromRepo, addressType);
        }

        [Test]
        public void CreateAddressType_GivenValidAddressType_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var addressType = AddressTypeBuilder.BuildRandom();
            var addressTypes = new List<AddressType>();
            var dbSet = DbSetSupport<AddressType>.CreateDbSetWithAddRemoveSupport(addressTypes);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var addressTypeService = CreateAddressTypeService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            addressTypeService.CreateAddressType(addressType);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void GetAddressTypeById_GivenIdIsNull_ShouldThrowExcption()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var addressTypeService = CreateAddressTypeService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                addressTypeService.GetAddressTypeById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("addressTypeID", ex.ParamName);
        }

        [Test]
        public void GetAddressTypeById_GivenValidId_ShoulReturnAddressTypeWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var addressType = new AddressTypeBuilder().WithRandomProps().Build();
            var dbSet = new FakeDbSet<AddressType> { addressType };
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var addressTypeService = CreateAddressTypeService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = addressTypeService.GetAddressTypeById(addressType.AddressTypeID);
            //---------------Test Result -----------------------
            Assert.AreEqual(addressType, result);
        }

        [Test]
        public void DeleteAddressType_GivenEmptyAddressType_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var addressTypeService = CreateAddressTypeService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                addressTypeService.DeleteAddressType(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("addressType", ex.ParamName);
        }

        [Test]
        public void DeleteAddressType_GivenValidAddressType_ShouldDeleteAddressType()
        {
            //---------------Set up test pack-------------------
            var addressTypes = new List<AddressType>();
            var addressType = AddressTypeBuilder.BuildRandom();

            var dbSet = DbSetSupport<AddressType>.CreateDbSetWithAddRemoveSupport(addressTypes);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var addressTypeService = CreateAddressTypeService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            addressTypeService.DeleteAddressType(addressType);
            //---------------Test Result -----------------------
            var addressTypesFromRepo = addressTypeService.GetAddressTypes();
            CollectionAssert.DoesNotContain(addressTypesFromRepo, addressType);
        }

        [Test]
        public void DeleteAddressType_GivenValidAddressType_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var addressTypes = new List<AddressType>();
            var addressType = AddressTypeBuilder.BuildRandom();

            var dbSet = DbSetSupport<AddressType>.CreateDbSetWithAddRemoveSupport(addressTypes);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var addressTypeService = CreateAddressTypeService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            addressTypeService.DeleteAddressType(addressType);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void UpdateAddressType_GivenInvalidExistingAddressType_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var addressTypeService = CreateAddressTypeService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => addressTypeService.UpdateAddressType(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("addressTypeToUpdate", ex.ParamName);
        }

        private static AddressTypeService CreateAddressTypeService(IApplicationDbContext applicationDbContext)
        {
            return new AddressTypeService(applicationDbContext);
        }

        private static IApplicationDbContext CreateApplicationDbContext(IDbSet<AddressType> dbSet = null)
        {
            if (dbSet == null) dbSet = DbSetSupport<AddressType>.CreateDbSetWithAddRemoveSupport(new List<AddressType>());
            var applicationDbContext = Substitute.For<IApplicationDbContext>();
            applicationDbContext.AddressTypes.Returns(_ => dbSet);
            return applicationDbContext;
        }
    }
}
