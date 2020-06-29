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
    public class TestAddressService
    {
        private ApplicationDbContext _dbContext;
        private IAddressService _addressService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase("testdb")
                                    .Options;
            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();

            _addressService = new AddressService(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext?.Database.EnsureDeleted();
        }
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------s
            //---------------Assert Precondition----------------
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
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _addressService.GetAddress();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetAddress_GivenAddressExistInRepo_ShouldReturnListOfAddress()
        {
            //---------------Set up test pack-------------------           
            SeedDb(1);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _addressService.GetAddress();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CreateAddress_GivenAddressIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _addressService.CreateAddress(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("address", ex.ParamName);
        }

        [Test]
        public void CreateAddress_GivenAddress_ShouldSaveAddress()
        {
            //---------------Set up test pack-------------------
            var address = AddressBuilder.BuildRandom();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _addressService.CreateAddress(address);
            //---------------Test Result -----------------------
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CreateAddress_GivenValidAddress_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var address = AddressBuilder.BuildRandom();
            var dbContext = Substitute.For<IApplicationDbContext>();
            _addressService = new AddressService(dbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _addressService.CreateAddress(address);
            //---------------Test Result -----------------------
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void GetAddressById_GivenIdIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _addressService.GetAddressById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("addressId", ex.ParamName);
        }

        [Test]
        public void GetAddressById_GivenValidId_ShouldReturnAddressWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var address = SeedDb(1).FirstOrDefault();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            if (address == null) return;
            var result = _addressService.GetAddressById(address.AddressId);
            //---------------Test Result -----------------------
            Assert.AreEqual(address, result);
        }

        [Test]
        public void DeleteAddress_GivenEmptyAddress_ShouldThrowException()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _addressService.DeleteAddress(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("address", ex.ParamName);
        }

        [Test]
        public void DeleteAddress_GivenValidAddress_ShouldDeleteAddress()
        {
            //---------------Set up test pack-------------------
            var address = SeedDb(1).FirstOrDefault();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            _addressService.DeleteAddress(address);
            //---------------Test Result -----------------------
            var addressFromRepo = _addressService.GetAddress();
            CollectionAssert.DoesNotContain(addressFromRepo, address);
        }

        [Test]
        public void DeleteAddress_GivenValidAddress_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var address = SeedDb(1).FirstOrDefault();
            var dbContext = Substitute.For<IApplicationDbContext>();
            _addressService = new AddressService(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            _addressService.DeleteAddress(address);
            //---------------Test Result -----------------------
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void UpdateAddress_GivenInvalidExistingAddress_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => _addressService.UpdateAddress(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("addressToUpdate", ex.ParamName);
        }

        private List<Address> SeedDb(int addressCount)
        {
            var addresses = new List<Address>();
            for (var a = 1; a <= addressCount; a++)
            {
                var address = new AddressBuilder().WithRandomProps().Build();
                addresses.Add(address);
            }

            foreach (var address in addresses)
                _addressService.CreateAddress(address);

            return addresses;
        }
    }
}
