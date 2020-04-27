using AutoMapper;
using QuoteMe.Contracts.V1.Responses;
using QuoteMe.DB.Domain;

namespace QuoteMe.Server.MappingProfile
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Client, ClientResponse>().ReverseMap();
        }
    }
}