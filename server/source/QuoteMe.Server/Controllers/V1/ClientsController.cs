using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.Contracts.V1;
using QuoteMe.Contracts.V1.Responses;
using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;

namespace QuoteMe.Server.Controllers.V1
{
    public class ClientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IClientService _clientService;
        private readonly IAddressService _addressService;

        public ClientsController(IClientService clientService, IMapper mapper, IAddressService addressService, IUriService uriService)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
            _uriService = uriService ?? throw new ArgumentNullException(nameof(uriService));
        }

        [HttpGet(ApiRoutes.Clients.GetAll)]
        public IActionResult GetAll()
        {
            var clients = _clientService.GetClients() ?? new List<Client>();
            var clientResponse = _mapper.Map<List<ClientResponse>>(clients);

            return Ok(clientResponse);
        }

        // GET: api/Clients/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Clients
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
