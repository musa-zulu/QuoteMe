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
    public class TestClientService
    {
        private ApplicationDbContext _dbContext;
        private IClientService _clientService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase("testdb")
                                    .Options;
            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();

            _clientService = new ClientService(_dbContext);
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
            Assert.DoesNotThrow(() => new ClientService(Substitute.For<IApplicationDbContext>()));
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenApplicationDbContextIsNull_ShouldThrow()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ClientService(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("applicationDbContext", ex.ParamName);
        }

        [Test]
        public void GetClients_GivenNoClientExist_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _clientService.GetClients();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetClients_GivenAClientExistInRepo_ShouldReturnThatClient()
        {
            //---------------Set up test pack------------------- 
            SeedDb(1);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _clientService.GetClients();
            //---------------Test Result -----------------------
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetClients_GivenClientsExistInRepo_ShouldReturnListOfClient()
        {
            //---------------Set up test pack------------------- 
            SeedDb(2);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _clientService.GetClients();
            //---------------Test Result -----------------------
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void CreateClient_GivenClientIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _clientService.CreateClient(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("client", ex.ParamName);
        }

        [Test]
        public void CreateClient_GivenClient_ShouldSaveClient()
        {
            //---------------Set up test pack-------------------
            var client = ClientBuilder.BuildRandom();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = _clientService.CreateClient(client);
            //---------------Test Result -----------------------       
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CreateClient_GivenValidClient_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var client = ClientBuilder.BuildRandom();
            var dbContext = Substitute.For<IApplicationDbContext>();
            _clientService = new ClientService(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            _clientService.CreateClient(client);
            //---------------Test Result -----------------------
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void GetClientById_GivenIdIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _clientService.GetClientById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("clientId", ex.ParamName);
        }

        [Test]
        public void GetClientById_GivenValidId_ShouldReturnClientWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var client = SeedDb(1).FirstOrDefault();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            if (client == null) return;
            var result = _clientService.GetClientById(client.ClientId);
            //---------------Test Result -----------------------
            Assert.AreEqual(client, result);
        }

        [Test]
        public void DeleteClient_GivenEmptyClient_ShouldThrowException()
        {
            //---------------Set up test pack-------------------     
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _clientService.DeleteClient(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("client", ex.ParamName);
        }

        [Test]
        public void DeleteClient_GivenValidClient_ShouldDeleteClient()
        {
            //---------------Set up test pack-------------------  
            var client = SeedDb(1).FirstOrDefault();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _clientService.DeleteClient(client);
            //---------------Test Result -----------------------
            var clientsFromRepo = _clientService.GetClients();
            CollectionAssert.DoesNotContain(clientsFromRepo, client);
        }

        [Test]
        public void DeleteClient_GivenValidClient_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var client = SeedDb(1).FirstOrDefault();
            var dbContext = Substitute.For<IApplicationDbContext>();
            _clientService = new ClientService(dbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _clientService.DeleteClient(client);
            //---------------Test Result -----------------------
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void UpdateClient_GivenInvalidExistingClient_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => _clientService.UpdateClient(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("clientToUpdate", ex.ParamName);
        }

        [Test]
        public void UpdateClient_GivenClientToUpdate_ShouldUpdateClient()
        {
            //---------------Set up test pack-------------------
            var client = SeedDb(2);
            var clientToUpdate = client.FirstOrDefault();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = _clientService.UpdateClient(clientToUpdate);
            //---------------Test Result -----------------------
            Assert.AreEqual(true, result);
        }

        [Test]
        public void UpdateClient_GivenClientToUpdate_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var clientToUpdate = SeedDb(1).FirstOrDefault();
            var dbContext = Substitute.For<IApplicationDbContext>();
            _clientService = new ClientService(dbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _clientService.UpdateClient(clientToUpdate);
            //---------------Test Result -----------------------
            dbContext.Received().SaveChanges();
        }

        private List<Client> SeedDb(int clientCount)
        {
            var clients = new List<Client>();
            for (var c = 1; c <= clientCount; c++)
            {
                var client = new ClientBuilder().WithRandomProps().Build();
                clients.Add(client);
            }

            foreach (var client in clients)
                _clientService.CreateClient(client);

            return clients;
        }

    }
}
