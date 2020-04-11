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
    public class TestPhoneNumberService
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition---------------

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
            var applicationDbContext = CreateApplicationDbContext();
            var phoneNumberService = CreatePhoneNumberService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = phoneNumberService.GetPhoneNumbers();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetPhoneNumbers_GivenPhoneNumbersExistInRepo_ShouldReturnListOfPhoneNumbers()
        {
            //---------------Set up test pack-------------------           
            var phoneNumbers = new List<Phone>();
            var dbSet = DbSetSupport<Phone>.CreateDbSetWithAddRemoveSupport(phoneNumbers);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var phoneNumberService = CreatePhoneNumberService(applicationDbContext);

            var phoneNumber = PhoneNumberBuilder.BuildRandom();
            phoneNumbers.Add(phoneNumber);
            dbSet.GetEnumerator().Returns(_ => phoneNumbers.GetEnumerator());
            applicationDbContext.PhoneNumbers.Returns(_ => dbSet);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = phoneNumberService.GetPhoneNumbers();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CreatePhoneNumber_GivenPhoneNumberIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var phoneNumberService = CreatePhoneNumberService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                phoneNumberService.CreatePhoneNumber(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("phone", ex.ParamName);
        }

        [Test]
        public void CreatePhoneNumber_GivenPhoneNumber_ShouldSavePhoneNumber()
        {
            //---------------Set up test pack-------------------
            var phoneNumber = PhoneNumberBuilder.BuildRandom();
            var phoneNumbers = new List<Phone>();

            var dbSet = DbSetSupport<Phone>.CreateDbSetWithAddRemoveSupport(phoneNumbers);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var phoneNumberService = CreatePhoneNumberService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            phoneNumberService.CreatePhoneNumber(phoneNumber);
            //---------------Test Result -----------------------
            var phoneNumbersFromRepo = phoneNumberService.GetPhoneNumbers();
            CollectionAssert.Contains(phoneNumbersFromRepo, phoneNumber);
        }

        [Test]
        public void CreatePhoneNumber_GivenValidPhoneNumber_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var phoneNumber = PhoneNumberBuilder.BuildRandom();
            var phoneNumbers = new List<Phone>();
            var dbSet = DbSetSupport<Phone>.CreateDbSetWithAddRemoveSupport(phoneNumbers);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var phoneNumberService = CreatePhoneNumberService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            phoneNumberService.CreatePhoneNumber(phoneNumber);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void GetPhoneNumberById_GivenIdIsNull_ShouldThrowExcption()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var phoneNumberService = CreatePhoneNumberService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                phoneNumberService.GetPhoneNumberById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("phoneID", ex.ParamName);
        }

        [Test]
        public void GetPhoneNumberById_GivenValidId_ShoulReturnPhoneNumberWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var phoneNumber = new PhoneNumberBuilder().WithRandomProps().Build();
            var dbSet = new FakeDbSet<Phone> { phoneNumber };
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var phoneNumberService = CreatePhoneNumberService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = phoneNumberService.GetPhoneNumberById(phoneNumber.PhoneID);
            //---------------Test Result -----------------------
            Assert.AreEqual(phoneNumber, result);
        }

        [Test]
        public void DeletePhoneNumber_GivenEmptyPhoneNumber_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var phoneNumberService = CreatePhoneNumberService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                phoneNumberService.DeletePhoneNumber(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("phone", ex.ParamName);
        }

        [Test]
        public void DeletePhoneNumber_GivenValidPhoneNumber_ShouldDeletePhoneNumber()
        {
            //---------------Set up test pack-------------------
            var phoneNumbers = new List<Phone>();
            var phoneNumber = PhoneNumberBuilder.BuildRandom();

            var dbSet = DbSetSupport<Phone>.CreateDbSetWithAddRemoveSupport(phoneNumbers);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var phoneNumberService = CreatePhoneNumberService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            phoneNumberService.DeletePhoneNumber(phoneNumber);
            //---------------Test Result -----------------------
            var phoneNumbersFromRepo = phoneNumberService.GetPhoneNumbers();
            CollectionAssert.DoesNotContain(phoneNumbersFromRepo, phoneNumber);
        }

        [Test]
        public void DeletePhoneNumber_GivenValidPhoneNumber_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var phoneNumbers = new List<Phone>();
            var phoneNumber = PhoneNumberBuilder.BuildRandom();

            var dbSet = DbSetSupport<Phone>.CreateDbSetWithAddRemoveSupport(phoneNumbers);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var phoneNumberService = CreatePhoneNumberService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            phoneNumberService.DeletePhoneNumber(phoneNumber);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void UpdatePhoneNumber_GivenInvalidExistingPhoneNumber_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var phoneNumberService = CreatePhoneNumberService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => phoneNumberService.UpdatePhoneNumber(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("phoneToUpdate", ex.ParamName);
        }

        private static PhoneNumberService CreatePhoneNumberService(IApplicationDbContext applicationDbContext)
        {
            return new PhoneNumberService(applicationDbContext);
        }

        private static IApplicationDbContext CreateApplicationDbContext(IDbSet<Phone> dbSet = null)
        {
            if (dbSet == null) dbSet = DbSetSupport<Phone>.CreateDbSetWithAddRemoveSupport(new List<Phone>());
            var applicationDbContext = Substitute.For<IApplicationDbContext>();
            applicationDbContext.PhoneNumbers.Returns(_ => dbSet);
            return applicationDbContext;
        }
    }
}
