using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.Contracts.V1.Responses;
using QuoteMe.DB.Domain;
using QuoteMe.Server.Controllers.V1;
using System;
using System.Collections.Generic;
using System.Reflection;
using QuoteMe.Server.Tests.Builders.Controllers;

namespace QuoteMe.Server.Tests.Controllers.V1
{
    [TestFixture]
    public class TestClientsController
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new ClientsController(Substitute.For<IClientService>(), Substitute.For<IMapper>(), Substitute.For<IAddressService>(), Substitute.For<IUriService>()));
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenIClientServiceIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ClientsController(null, Substitute.For<IMapper>(), Substitute.For<IAddressService>(), Substitute.For<IUriService>()));
            //---------------Test Result -----------------------
            Assert.AreEqual("clientService", ex.ParamName);
        }

        [Test]
        public void Construct_GivenIMapperIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ClientsController(Substitute.For<IClientService>(), null, Substitute.For<IAddressService>(), Substitute.For<IUriService>()));
            //---------------Test Result -----------------------
            Assert.AreEqual("mapper", ex.ParamName);
        }

        [Test]
        public void Construct_GivenIAddressServiceIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ClientsController(Substitute.For<IClientService>(), Substitute.For<IMapper>(), null, Substitute.For<IUriService>()));
            //---------------Test Result -----------------------
            Assert.AreEqual("addressService", ex.ParamName);
        }

        [Test]
        public void Construct_GivenIUriServiceIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ClientsController(Substitute.For<IClientService>(), Substitute.For<IMapper>(), Substitute.For<IAddressService>(), null));
            //---------------Test Result -----------------------
            Assert.AreEqual("uriService", ex.ParamName);
        }

        [Test]
        public void GetAll_ShouldHaveHttpGetAttribute()
        {
            //---------------Set up test pack-------------------
            var methodInfo = typeof(ClientsController).GetMethod("GetAll");
            //---------------Assert Precondition----------------
            Assert.IsNotNull(methodInfo);
            //---------------Execute Test ----------------------
            var httpGetAttribute = methodInfo.GetCustomAttribute<HttpGetAttribute>();
            //---------------Test Result -----------------------
            Assert.NotNull(httpGetAttribute);
        }

        [Test]
        public void GetAll_ShouldReturnIActionResult()
        {
            //---------------Set up test pack-------------------
            var clientsController = CreateClientsControllerBuilder()
                                    .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = clientsController.GetAll();
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAll_ShouldCallGetClients()
        {
            //---------------Set up test pack-------------------
            var clientsService = Substitute.For<IClientService>();

            var clientsController = CreateClientsControllerBuilder()
                .WithClientService(clientsService)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _ = clientsController.GetAll();
            //---------------Test Result -----------------------
            clientsService.Received(1).GetClients();
        }

        [Test]
        public void Get_ShouldCallMapper()
        {
            //---------------Set up test pack-------------------
            var mapper = Substitute.For<IMapper>();
            var clientService = Substitute.For<IClientService>();
            var clients = new List<Client>();
            clientService.GetClients().Returns(clients);

            var clientsController = CreateClientsControllerBuilder()
                .WithClientService(clientService)
                .WithMapper(mapper)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _ = clientsController.GetAll();
            //---------------Test Result -----------------------
            mapper.Received(1).Map<List<ClientResponse>>(clients);
        }

        private static ClientsControllerBuilder CreateClientsControllerBuilder()
        {
            return new ClientsControllerBuilder();
        }

    }
}
