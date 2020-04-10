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
    public class TestClientService
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition---------------

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
            var applicationDbContext = CreateApplicationDbContext();
            var clientService = CreateClientService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = clientService.GetClients();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetClients_GivenClientsExistInRepo_ShouldReturnListOfClients()
        {
            //---------------Set up test pack-------------------           
            var clients = new List<Client>();
            var dbSet = CreateDbSetWithAddRemoveSupport(clients);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var clientService = CreateClientService(applicationDbContext);

            var client = ClientBuilder.BuildRandom();
            clients.Add(client);
            dbSet.GetEnumerator().Returns(_ => clients.GetEnumerator());
            applicationDbContext.Clients.Returns(_ => dbSet);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = clientService.GetClients();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CreateClient_GivenClientIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var clientService = CreateClientService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                clientService.CreateClient(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("client", ex.ParamName);
        }

        [Test]
        public void CreateClient_GivenClient_ShouldSaveClient()
        {
            //---------------Set up test pack-------------------
            var client = ClientBuilder.BuildRandom();
            var clients = new List<Client>();

            var dbSet = CreateDbSetWithAddRemoveSupport(clients);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var clientService = CreateClientService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            clientService.CreateClient(client);
            //---------------Test Result -----------------------
            var clientsFromRepo = clientService.GetClients();
            CollectionAssert.Contains(clientsFromRepo, client);
        }

        [Test]
        public void CreateClient_GivenValidClient_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var client = ClientBuilder.BuildRandom();
            var clients = new List<Client>();
            var dbSet = CreateDbSetWithAddRemoveSupport(clients);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var clientService = CreateClientService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            clientService.CreateClient(client);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void GetClientById_GivenIdIsNull_ShouldThrowExcption()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var clientService = CreateClientService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                clientService.GetClientById(Guid.Empty);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("clientID", ex.ParamName);
        }

        [Test]
        public void GetClientById_GivenValidId_ShoulReturnClientWithMatchingId()
        {
            //---------------Set up test pack-------------------
            var client = new ClientBuilder().WithRandomProps().Build();
            var dbSet = new FakeDbSet<Client> { client };
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var clientService = CreateClientService(applicationDbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = clientService.GetClientById(client.ClientID);
            //---------------Test Result -----------------------
            Assert.AreEqual(client, result);
        }

        [Test]
        public void DeleteClient_GivenEmptyClient_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var clientService = CreateClientService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                clientService.DeleteClient(null);
            });
            //---------------Test Result -----------------------
            Assert.AreEqual("client", ex.ParamName);
        }

        [Test]
        public void DeleteClient_GivenValidClient_ShouldDeleteClient()
        {
            //---------------Set up test pack-------------------
            var clients = new List<Client>();
            var client = ClientBuilder.BuildRandom();

            var dbSet = CreateDbSetWithAddRemoveSupport(clients);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var clientService = CreateClientService(applicationDbContext);

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            clientService.DeleteClient(client);
            //---------------Test Result -----------------------
            var clientsFromRepo = clientService.GetClients();
            CollectionAssert.DoesNotContain(clientsFromRepo, client);
        }

        [Test]
        public void DeleteClient_GivenValidClient_ShouldCallSaveChanges()
        {
            //---------------Set up test pack-------------------
            var clients = new List<Client>();
            var client = ClientBuilder.BuildRandom();

            var dbSet = CreateDbSetWithAddRemoveSupport(clients);
            var applicationDbContext = CreateApplicationDbContext(dbSet);
            var clientService = CreateClientService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            clientService.DeleteClient(client);
            //---------------Test Result -----------------------
            applicationDbContext.Received().SaveChanges();
        }

        [Test]
        public void UpdateClient_GivenInvalidExistingClient_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var applicationDbContext = CreateApplicationDbContext();
            var clientService = CreateClientService(applicationDbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => clientService.UpdateClient(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("clientToUpdate", ex.ParamName);
        }

        private static ClientService CreateClientService(IApplicationDbContext applicationDbContext)
        {
            return new ClientService(applicationDbContext);
        }

        private static IDbSet<Client> CreateDbSetWithAddRemoveSupport(List<Client> clients)
        {
            var dbSet = Substitute.For<IDbSet<Client>>();

            dbSet.Add(Arg.Any<Client>()).Returns(info =>
            {
                clients.Add(info.ArgAt<Client>(0));
                return info.ArgAt<Client>(0);
            });

            dbSet.Remove(Arg.Any<Client>()).Returns(info =>
            {
                clients.Remove(info.ArgAt<Client>(0));
                return info.ArgAt<Client>(0);
            });

            dbSet.GetEnumerator().Returns(_ => clients.GetEnumerator());
            return dbSet;
        }

        private static IApplicationDbContext CreateApplicationDbContext(IDbSet<Client> dbSet = null)
        {
            if (dbSet == null) dbSet = CreateDbSetWithAddRemoveSupport(new List<Client>());
            var applicationDbContext = Substitute.For<IApplicationDbContext>();
            applicationDbContext.Clients.Returns(_ => dbSet);
            return applicationDbContext;
        }

    }
}
