using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.Contracts.V1;
using QuoteMe.Contracts.V1.Responses;
using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;
using QuoteMe.Contracts.Helpers;
using QuoteMe.Contracts.V1.Requests;

namespace QuoteMe.Server.Controllers.V1
{
    public class ClientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IClientService _clientService;
        private readonly IAddressService _addressService;
        private IDateTimeProvider _dateTimeProvider;

        public ClientsController(IClientService clientService, IMapper mapper, IAddressService addressService, IUriService uriService)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
            _uriService = uriService ?? throw new ArgumentNullException(nameof(uriService));
        }
        
        public IDateTimeProvider DateTimeProvider
        {
            get => _dateTimeProvider ?? new DefaultDateTimeProvider();
            set
            {
                if (_dateTimeProvider != null) throw new InvalidOperationException("DateTimeProvider is already set");
                _dateTimeProvider = value;
            }
        }

        [HttpGet(ApiRoutes.Clients.GetAll)]
        public IActionResult GetAll()
        {
            var clients = _clientService.GetClients() ?? new List<Client>();
            var clientResponse = _mapper.Map<List<ClientResponse>>(clients);

            return Ok(clientResponse);
        }

       [HttpGet(ApiRoutes.Clients.Get)]
        public IActionResult Get([FromRoute]Guid clientId)
        {
            var client =  _clientService.GetClientById(clientId);

            if (client == null)
                return NotFound();

            return Ok(new Response<ClientResponse>(_mapper.Map<ClientResponse>(client)));
        }

        [HttpPost(ApiRoutes.Clients.Create)]
        public IActionResult Create([FromBody] CreateClientRequest postRequest)
        {
            SetDefaultFieldsFor(postRequest);

            var client = _mapper.Map<CreateClientRequest, Client>(postRequest);

            _clientService.CreateClient(client);

            var locationUri = _uriService.GetAllUri();
            return Created(locationUri, new Response<ClientResponse>(_mapper.Map<ClientResponse>(client)));
        }

        [HttpPut(ApiRoutes.Clients.Update)]
        public IActionResult Update([FromBody] UpdateClientRequest request)
        {
            if (request.ClientId == Guid.Empty)
            {
                return BadRequest(new ErrorResponse(new ErrorModel { Message = "The client does not exist, or the id is empty." }));
            }
            
            UpdateBaseFieldsOn(request);
            
            var client = _mapper.Map<UpdateClientRequest, Client>(request);
            if (client != null)
                client.ClientId = request.ClientId;
            
            var isUpdated = _clientService.UpdateClient(client);

            if (isUpdated)
                return Ok(new Response<ClientResponse>(_mapper.Map<ClientResponse>(client)));

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Clients.Delete)]
        public IActionResult Delete([FromRoute] Guid clientId)
        {
            if (clientId == Guid.Empty)
                return NoContent();

            var client = _clientService.GetClientById(clientId);
            var deleted = _clientService.DeleteClient(client);

            if (deleted)
                return NoContent();

            return NotFound();
        }
        
        private void SetDefaultFieldsFor(CreateClientRequest postRequest)
        {
            postRequest.ClientId = Guid.NewGuid();
            postRequest.DateCreated = DateTimeProvider.Now;
            postRequest.DateLastModified = DateTimeProvider.Now;
        }

        private void UpdateBaseFieldsOn(UpdateClientRequest request)
        {
            request.DateLastModified = DateTimeProvider.Now;
        }
    }
}