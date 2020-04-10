using QuoteMe.Contracts.Interfaces.Services;
using System;

namespace QuoteMe.Contracts.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetAllUri()
        {
            return new Uri(_baseUri);
        }
    }
}