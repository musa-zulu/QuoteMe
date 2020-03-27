using QuoteMe.Contracts.V1.Requests.Queries;
using System;

namespace QuoteMe.Contracts.Interfaces.Services
{
    public interface IUriService
    {
        Uri GetAllUri(PaginationQuery pagination = null);
    }
}
