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
    public class TestPhoneNumberService
    {
        private ApplicationDbContext _dbContext;
        private IPhoneNumberService _phoneNumberService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase("testdb")
                                    .Options;
            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();

            _phoneNumberService = new PhoneNumberService(_dbContext);
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
            Assert.DoesNotThrow(() => new PhoneNumberService(Substitute.For<IApplicationDbContext>()));
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenApplicationDbContextIsNull_ShouldThrow()
        {

            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new PhoneNumberService(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("applicationDbContext", ex.ParamName);
        }

        [Test]
        public void GetPhoneNumbers_GivenNoPhoneNumberExist_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _phoneNumberService.GetPhoneNumbers();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetPhoneNumbers_GivenPhoneNumbersExistInRepo_ShouldReturnListOfPhoneNumbers()
        {
            //---------------Set up test pack-------------------           
            SeedDb(1);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _phoneNumberService.GetPhoneNumbers();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CreatePhoneNumber_GivenPhoneNumberIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _phoneNumberService.CreatePhoneNumber(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("phone", ex.ParamName);
        }

        [Test]
        public void CreatePhoneNumber_GivenPhoneNumber_ShouldSavePhoneNumber()
        {
            //---------------Set up test pack-------------------
            var phoneNumber = PhoneNumberBuilder.BuildRandom();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _phoneNumberService.CreatePhoneNumber(phoneNumber);
            //---------------Test Result -----------------------
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CreatePhoneNumber_GivenValidPhoneNumber_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var phoneNumber = PhoneNumberBuilder.BuildRandom();
            var _dbContext = Substitute.For<IApplicationDbContext>();
            _phoneNumberService = new PhoneNumberService(_dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            _phoneNumberService.CreatePhoneNumber(phoneNumber);
            //---------------Test Result -----------------------
            _dbContext.Received().SaveChanges();
        }

        [Test]
        public void GetPhoneNumberById_GivenIdIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _phoneNumberService.GetPhoneNumberById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("phoneId", ex.ParamName);
        }

        [Test]
        public void GetPhoneNumberById_GivenValidId_ShouldReturnPhoneNumberWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var phoneNumber = SeedDb(1).FirstOrDefault();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            if (phoneNumber == null) return;
            var result = _phoneNumberService.GetPhoneNumberById(phoneNumber.PhoneId);
            //---------------Test Result -----------------------
            Assert.AreEqual(phoneNumber, result);
        }

        [Test]
        public void DeletePhoneNumber_GivenEmptyPhoneNumber_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _phoneNumberService.DeletePhoneNumber(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("phone", ex.ParamName);
        }

        [Test]
        public void DeletePhoneNumber_GivenValidPhoneNumber_ShouldDeletePhoneNumber()
        {
            //---------------Set up test pack-------------------          
            var phoneNumber = SeedDb(1).FirstOrDefault();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _phoneNumberService.DeletePhoneNumber(phoneNumber);
            //---------------Test Result -----------------------
            var phoneNumbersFromRepo = _phoneNumberService.GetPhoneNumbers();
            CollectionAssert.DoesNotContain(phoneNumbersFromRepo, phoneNumber);
        }

        [Test]
        public void DeletePhoneNumber_GivenValidPhoneNumber_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var phoneNumber = PhoneNumberBuilder.BuildRandom();
            var dbContext = Substitute.For<IApplicationDbContext>();
            _phoneNumberService = new PhoneNumberService(dbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _phoneNumberService.DeletePhoneNumber(phoneNumber);
            //---------------Test Result -----------------------
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void UpdatePhoneNumber_GivenInvalidExistingPhoneNumber_ShouldThrowException()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => _phoneNumberService.UpdatePhoneNumber(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("phoneToUpdate", ex.ParamName);
        }

        private List<Phone> SeedDb(int phoneCount)
        {
            var phoneNumbers = new List<Phone>();
            for (var c = 1; c <= phoneCount; c++)
            {
                var phone = new PhoneNumberBuilder().WithRandomProps().Build();
                phoneNumbers.Add(phone);
            }

            foreach (var phone in phoneNumbers)
                _phoneNumberService.CreatePhoneNumber(phone);

            return phoneNumbers;
        }
    }
}
