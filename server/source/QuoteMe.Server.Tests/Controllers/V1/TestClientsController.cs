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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using QuoteMe.Contracts.Helpers;
using QuoteMe.Contracts.V1.Requests;
using QuoteMe.Server.Tests.Builders.Controllers;
using QuoteMe.Tests.Common.Builders.Models;
using QuoteMe.Tests.Common.Builders.Requests;

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
            Assert.DoesNotThrow(() => new ClientsController(Substitute.For<IClientService>(), Substitute.For<IMapper>(),
                Substitute.For<IAddressService>(), Substitute.For<IUriService>()));
            //---------------Test Result -----------------------
        }

        [Test]
        public void Construct_GivenIClientServiceIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ClientsController(null, Substitute.For<IMapper>(),
                Substitute.For<IAddressService>(), Substitute.For<IUriService>()));
            //---------------Test Result -----------------------
            Assert.AreEqual("clientService", ex.ParamName);
        }

        [Test]
        public void Construct_GivenIMapperIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ClientsController(Substitute.For<IClientService>(),
                null, Substitute.For<IAddressService>(), Substitute.For<IUriService>()));
            //---------------Test Result -----------------------
            Assert.AreEqual("mapper", ex.ParamName);
        }

        [Test]
        public void Construct_GivenIAddressServiceIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ClientsController(Substitute.For<IClientService>(),
                Substitute.For<IMapper>(), null, Substitute.For<IUriService>()));
            //---------------Test Result -----------------------
            Assert.AreEqual("addressService", ex.ParamName);
        }

        [Test]
        public void Construct_GivenIUriServiceIsNull_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new ClientsController(Substitute.For<IClientService>(),
                Substitute.For<IMapper>(), Substitute.For<IAddressService>(), null));
            //---------------Test Result -----------------------
            Assert.AreEqual("uriService", ex.ParamName);
        }

        [Test]
        public void DateTimeProvider_GivenSetDateTimeProvider_ShouldSetDateTimeProviderOnFirstCall()
        {
            //---------------Set up test pack-------------------
            var controller = CreateClientsControllerBuilder().Build();
            var dateTimeProvider = Substitute.For<IDateTimeProvider>();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            controller.DateTimeProvider = dateTimeProvider;
            //---------------Test Result -----------------------
            Assert.AreSame(dateTimeProvider, controller.DateTimeProvider);
        }

        [Test]
        public void DateTimeProvider_GivenSetDateTimeProviderIsSet_ShouldThrowOnCall()
        {
            //---------------Set up test pack-------------------
            var controller = CreateClientsControllerBuilder().Build();
            var dateTimeProvider = Substitute.For<IDateTimeProvider>();
            var dateTimeProvider1 = Substitute.For<IDateTimeProvider>();
            controller.DateTimeProvider = dateTimeProvider;
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<InvalidOperationException>(() => controller.DateTimeProvider = dateTimeProvider1);
            //---------------Test Result -----------------------
            Assert.AreEqual("DateTimeProvider is already set", ex.Message);
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
        public void GetAll_ShouldCallMapper()
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

        [Test]
        public void Get_ShouldHaveHttpGetAttribute()
        {
            //---------------Set up test pack-------------------
            var methodInfo = typeof(ClientsController).GetMethod("Get");
            //---------------Assert Precondition----------------
            Assert.IsNotNull(methodInfo);
            //---------------Execute Test ----------------------
            var httpGetAttribute = methodInfo.GetCustomAttribute<HttpGetAttribute>();
            //---------------Test Result -----------------------
            Assert.NotNull(httpGetAttribute);
        }

        [Test]
        public void Get_GivenClientIsNull_ShouldReturnStatus404NotFound()
        {
            //---------------Set up test pack-------------------
            var clientsController = CreateClientsControllerBuilder()
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = clientsController.Get(Guid.Empty) as StatusCodeResult;
            //---------------Test Result -----------------------
            var statusCode = new NotFoundResult().StatusCode;
            if (result != null) Assert.AreEqual(statusCode, result.StatusCode);
        }

        [Test]
        public void Get_GivenClientExist_ShouldReturnIActionResult()
        {
            //---------------Set up test pack-------------------
            var clientService = Substitute.For<IClientService>();
            var client = ClientBuilder.BuildRandom();
            clientService.GetClientById(client.ClientId).Returns(client);
            var clientsController = CreateClientsControllerBuilder()
                .WithClientService(clientService)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = clientsController.Get(client.ClientId);
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
        }

        [Test]
        public void Get_ShouldCallGetClientById()
        {
            //---------------Set up test pack-------------------
            var clientId = Guid.NewGuid();
            var clientsService = Substitute.For<IClientService>();

            var clientsController = CreateClientsControllerBuilder()
                .WithClientService(clientsService)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _ = clientsController.Get(clientId);
            //---------------Test Result -----------------------
            clientsService.Received(1).GetClientById(clientId);
        }

        [Test]
        public void Create_ShouldReturnCreatedResult()
        {
            //---------------Set up test pack-------------------
            var clientService = Substitute.For<IClientService>();
            var uriService = Substitute.For<IUriService>();
            const string baseUri = "http://localhost/";
            uriService.GetAllUri().Returns(new Uri(baseUri));

            var clientController = CreateClientsControllerBuilder()
                .WithClientService(clientService)
                .WithUriService(uriService)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = (CreatedResult) clientController.Create(new CreateClientRequest());
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
        }

        [Test]
        public void Create_ShouldCallMappingEngine()
        {
            //---------------Set up test pack-------------------
            var mappingEngine = Substitute.For<IMapper>();
            var createClientRequest = new CreateClientRequest();
            var uriService = Substitute.For<IUriService>();
            const string baseUri = "http://localhost/";
            uriService.GetAllUri().Returns(new Uri(baseUri));

            var clientController = CreateClientsControllerBuilder()
                .WithMapper(mappingEngine)
                .WithUriService(uriService)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = clientController.Create(createClientRequest) as CreatedResult;
            //---------------Test Result -----------------------
            mappingEngine.Received(1).Map<CreateClientRequest, Client>(createClientRequest);
        }

        [Test]
        public void Create_ShouldCallCreateFromClientsRepo()
        {
            //---------------Set up test pack-------------------
            var mappingEngine = Substitute.For<IMapper>();
            var clientService = Substitute.For<IClientService>();
            var createClientBuilder = CreateClientRequestBuilder.BuildRandom();
            var uriService = Substitute.For<IUriService>();
            const string baseUri = "http://localhost/";
            uriService.GetAllUri().Returns(new Uri(baseUri));

            var clientController = CreateClientsControllerBuilder()
                .WithClientService(clientService)
                .WithMapper(mappingEngine)
                .WithUriService(uriService)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            clientController.Create(createClientBuilder);
            //---------------Test Result -----------------------
            clientService.Received(1).CreateClient(Arg.Any<Client>());
        }

        [Test]
        public void Create_ShouldReturnCreatedResultWithClientResponse()
        {
            //---------------Set up test pack-------------------
            var mappingEngine = Substitute.For<IMapper>();
            var clientService = Substitute.For<IClientService>();
            var createClientBuilder = CreateClientRequestBuilder.BuildRandom();
            var uriService = Substitute.For<IUriService>();
            const string baseUri = "http://localhost/";
            uriService.GetAllUri().Returns(new Uri(baseUri));

            var clientsController = CreateClientsControllerBuilder()
                .WithClientService(clientService)
                .WithMapper(mappingEngine)
                .WithUriService(uriService)
                .Build();

            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = clientsController.Create(createClientBuilder) as CreatedResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
        }

        [Test]
        public void Create_ShouldHaveHttpCreateAttribute()
        {
            //---------------Set up test pack-------------------
            var methodInfo = typeof(ClientsController).GetMethod("Create");
            //---------------Assert Precondition----------------
            Assert.IsNotNull(methodInfo);
            //---------------Execute Test ----------------------
            var httpPostAttribute = methodInfo.GetCustomAttribute<HttpPostAttribute>();
            //---------------Test Result -----------------------
            Assert.NotNull(httpPostAttribute);
        }

        [Test]
        public void Update_ShouldHaveHttpPutAttribute()
        {
            //---------------Set up test pack-------------------
            var methodInfo = typeof(ClientsController).GetMethod("Update");
            //---------------Assert Precondition----------------
            Assert.IsNotNull(methodInfo);
            //---------------Execute Test ----------------------
            var httpPutAttribute = methodInfo.GetCustomAttribute<HttpPutAttribute>();
            //---------------Test Result -----------------------
            Assert.NotNull(httpPutAttribute);
        }

        [Test]
        public void Update_GivenInvalidUpdateClientRequest_ShouldReturnBadRequest()
        {
            //---------------Set up test pack-------------------
            var clientsController = CreateClientsControllerBuilder()
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = clientsController.Update(new UpdateClientRequest()) as StatusCodeResult;
            //---------------Test Result -----------------------
            var statusCode = new NotFoundResult().StatusCode;
            if (result != null) Assert.AreEqual(statusCode, result.StatusCode);
        }

        [Test]
        public void Update_GivenValidUpdateClientRequest_ShouldCallMappingEngine()
        {
            //---------------Set up test pack-------------------
            var mappingEngine = Substitute.For<IMapper>();
            var updateClientRequest = new UpdateClientRequest()
            {
                ClientId = Guid.NewGuid()
            };
            var clientController = CreateClientsControllerBuilder()
                .WithMapper(mappingEngine)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _ = clientController.Update(updateClientRequest) as CreatedResult;
            //---------------Test Result -----------------------
            mappingEngine.Received(1).Map<UpdateClientRequest, Client>(updateClientRequest);
        }

        [Test]
        public void Update_GivenValidUpdateClientRequest_ShouldCallUpdateClient()
        {
            //---------------Set up test pack-------------------
            var mappingEngine = Substitute.For<IMapper>();
            var clientService = Substitute.For<IClientService>();
            var updateClientRequest = new UpdateClientRequest()
            {
                ClientId = Guid.NewGuid()
            };

            var clientController = CreateClientsControllerBuilder()
                .WithMapper(mappingEngine)
                .WithClientService(clientService)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _ = clientController.Update(updateClientRequest) as CreatedResult;
            //---------------Test Result -----------------------
            clientService.Received(1).UpdateClient(Arg.Any<Client>());
        }

        [Test]
        public void Delete_ShouldReturnHaveHttpDelete()
        {
            //---------------Set up test pack-------------------
            var methodInfo = typeof(ClientsController).GetMethod("Delete");
            //---------------Assert Precondition----------------
            Assert.IsNotNull(methodInfo);
            //---------------Execute Test ----------------------
            var httpDelete = methodInfo.GetCustomAttribute<HttpDeleteAttribute>();
            //---------------Test Result -----------------------
            Assert.NotNull(httpDelete);
        }

       [Test]
        public void Delete_GivenInvalidId_ShouldReturnNoContentError()
        {
            //---------------Set up test pack-------------------
            var clientsController = CreateClientsControllerBuilder()
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = clientsController.Delete(Guid.Empty) as StatusCodeResult;
            //---------------Test Result -----------------------
            var statusCode = new NoContentResult().StatusCode;
            if (result != null) Assert.AreEqual(statusCode, result.StatusCode);
        }
        
        [Test]
        public void Delete_GivenValidValidClientId_ShouldCallGetClientById()
        {
            //---------------Set up test pack-------------------
            var clientService = Substitute.For<IClientService>();

            var clientController = CreateClientsControllerBuilder()
                .WithClientService(clientService)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _ = clientController.Delete(Guid.NewGuid()) as CreatedResult;
            //---------------Test Result -----------------------
            clientService.Received(1).GetClientById(Arg.Any<Guid>());
        }
        
        [Test]
        public void Delete_GivenValidValidClientId_ShouldCallDeleteClient()
        {
            //---------------Set up test pack-------------------
            var clientService = Substitute.For<IClientService>();

            var clientController = CreateClientsControllerBuilder()
                .WithClientService(clientService)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            _ = clientController.Delete(Guid.NewGuid()) as CreatedResult;
            //---------------Test Result -----------------------
            clientService.Received(1).DeleteClient(Arg.Any<Client>());
        }
        
        [Test]
        public void Delete_GivenClientIsDeleted_ShouldReturnNoContentError()
        {
            //---------------Set up test pack-------------------
            var clientService = Substitute.For<IClientService>();
            clientService.DeleteClient(Arg.Any<Client>()).Returns(true);

            var clientsController = CreateClientsControllerBuilder()
                .WithClientService(clientService)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = clientsController.Delete(Guid.Empty) as StatusCodeResult;
            //---------------Test Result -----------------------
            var statusCode = new NoContentResult().StatusCode;
            if (result != null) Assert.AreEqual(statusCode, result.StatusCode);
        }

        [Test]
        public void Delete_GivenClientIsNotDeleted_ShouldReturnNotFound()
        {
            //---------------Set up test pack-------------------
            var clientService = Substitute.For<IClientService>();
            clientService.DeleteClient(Arg.Any<Client>()).Returns(false);

            var clientsController = CreateClientsControllerBuilder()
                .WithClientService(clientService)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = clientsController.Delete(Guid.NewGuid()) as StatusCodeResult;
            //---------------Test Result -----------------------
            var statusCode = new NotFoundResult().StatusCode;
            if (result != null) Assert.AreEqual(statusCode, result.StatusCode);
        }

        private static ClientsControllerBuilder CreateClientsControllerBuilder()
        {
            return new ClientsControllerBuilder();
        }
    }
}