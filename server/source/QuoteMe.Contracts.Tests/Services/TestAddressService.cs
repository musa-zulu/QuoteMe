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
    public class TestAddressService
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
        public void GetAddress_GivenNoAddressExist_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var addressService = CreateAddressService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = addressService.GetAddress();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetAddress_GivenAddressExistInRepo_ShouldReturnListOfAddress()
        {
            //---------------Set up test pack-------------------           
            var addressList = new List<Address>();
            var dbSet = CreateDbSetWithAddRemoveSupport(addressList);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var addressService = CreateAddressService(applicationDbContext);

            var address = AddressBuilder.BuildRandom();
            addressList.Add(address);
            dbSet.GetEnumerator().Returns(_ => addressList.GetEnumerator());
            applicationDbContext.Addresses.Returns(_ => dbSet);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = addressService.GetAddress();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CreateAddress_GivenAddressIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var addressService = CreateAddressService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                addressService.CreateAddress(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("address", ex.ParamName);
        }

        [Test]
        public void CreateAddress_GivenAddress_ShouldSaveAdddress()
        {
            //---------------Set up test pack-------------------
            var address = AddressBuilder.BuildRandom();
            var addressList = new List<Address>();

            var dbSet = CreateDbSetWithAddRemoveSupport(addressList);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var addressService = CreateAddressService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            addressService.CreateAddress(address);
            //---------------Test Result -----------------------
            var addressFromRepo = addressService.GetAddress();
            CollectionAssert.Contains(addressFromRepo, address);
        }

        [Test]
        public void CreateAddress_GivenValidAddress_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var address = AddressBuilder.BuildRandom();
            var addressList = new List<Address>();
            var dbSet = CreateDbSetWithAddRemoveSupport(addressList);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var addressService = CreateAddressService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            addressService.CreateAddress(address);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void GetAddressById_GivenIdIsNull_ShouldThrowExcption()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var addressService = CreateAddressService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                addressService.GetAddressById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("addressID", ex.ParamName);
        }

        [Test]
        public void GetAddressById_GivenValidId_ShoulReturnAddressWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var address = new AddressBuilder().WithRandomProps().Build();
            var dbSet = new FakeDbSet<Address> { address };
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var addressService = CreateAddressService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = addressService.GetAddressById(address.AddressID);
            //---------------Test Result -----------------------
            Assert.AreEqual(address, result);
        }

        [Test]
        public void DeleteAddress_GivenEmptyAddress_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var addressService = CreateAddressService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                addressService.DeleteAddress(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("address", ex.ParamName);
        }

        [Test]
        public void DeleteAddress_GivenValidAddress_ShouldDeleteAddress()
        {
            //---------------Set up test pack-------------------
            var addressList = new List<Address>();
            var address = AddressBuilder.BuildRandom();

            var dbSet = CreateDbSetWithAddRemoveSupport(addressList);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var addressService = CreateAddressService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            addressService.DeleteAddress(address);
            //---------------Test Result -----------------------
            var addressFromRepo = addressService.GetAddress();
            CollectionAssert.DoesNotContain(addressFromRepo, address);
        }

        [Test]
        public void DeleteAddress_GivenValidAddress_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var addressList = new List<Address>();
            var address = AddressBuilder.BuildRandom();

            var dbSet = CreateDbSetWithAddRemoveSupport(addressList);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var addressService = CreateAddressService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            addressService.DeleteAddress(address);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void UpdateAddress_GivenInvalidExistingAddress_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var addressService = CreateAddressService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => addressService.UpdateAddress(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("addressToUpdate", ex.ParamName);
        }

        private static AddressService CreateAddressService(IApplicationDbContext applicationDbContext)
        {
            return new AddressService(applicationDbContext);
        }

        private static IDbSet<Address> CreateDbSetWithAddRemoveSupport(List<Address> addresses)
        {
            var dbSet = Substitute.For<IDbSet<Address>>();

            dbSet.Add(Arg.Any<Address>()).Returns(info =>
            {
                addresses.Add(info.ArgAt<Address>(0));
                return info.ArgAt<Address>(0);
            });

            dbSet.Remove(Arg.Any<Address>()).Returns(info =>
            {
                addresses.Remove(info.ArgAt<Address>(0));
                return info.ArgAt<Address>(0);
            });

            dbSet.GetEnumerator().Returns(_ => addresses.GetEnumerator());
            return dbSet;
        }

        private static IApplicationDbContext CreateApplicationDbContext(IDbSet<Address> dbSet = null)
        {
            if (dbSet == null) dbSet = CreateDbSetWithAddRemoveSupport(new List<Address>());
            var applicationDbContext = Substitute.For<IApplicationDbContext>();
            applicationDbContext.Addresses.Returns(_ => dbSet);
            return applicationDbContext;
        }

    }
}
