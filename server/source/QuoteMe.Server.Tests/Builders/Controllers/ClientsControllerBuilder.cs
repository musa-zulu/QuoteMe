using AutoMapper;
using NSubstitute;
using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.Server.Controllers.V1;

namespace QuoteMe.Tests.Common.Builders.Controllers
{
    public class ClientsControllerBuilder
    {
        public ClientsControllerBuilder()
        {
            Mapper = Substitute.For<IMapper>();
            UriService = Substitute.For<IUriService>();
            ClientService = Substitute.For<IClientService>();
            AddressService = Substitute.For<IAddressService>();
        }

        public IClientService ClientService { get; private set; }
        public IAddressService AddressService { get; private set; }
        public IMapper Mapper { get; private set; }
        public IUriService UriService { get; private set; }

        public ClientsControllerBuilder WithMapper(IMapper mapper)
        {
            Mapper = mapper;
            return this;
        }

        public ClientsControllerBuilder WithUriService(IUriService uriService)
        {
            UriService = uriService;
            return this;
        }

        public ClientsControllerBuilder WithAddressService(IAddressService addressService)
        {
            AddressService = addressService;
            return this;
        }

        public ClientsControllerBuilder WithClientService(IClientService clientService)
        {
            ClientService = clientService;
            return this;
        }

        public ClientsController Build()
        {
            return new ClientsController(ClientService, Mapper, AddressService, UriService);
        }
    }
}
