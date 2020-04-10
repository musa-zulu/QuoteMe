using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.Contracts.V1.Responses;
using QuoteMe.DB.Domain;
using System.Collections.Generic;
using System.Linq;

namespace QuoteMe.Server.Helpers
{
    public static class PaginationHelpers
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, PaginationFilter pagination, List<T> response)
        {
            var nextPage = pagination.PageNumber >= 1
                ? uriService.GetAllUri().ToString()
                : null;

            var previousPage = pagination.PageNumber - 1 >= 1
                ? uriService.GetAllUri().ToString()
                : null;

            return new PagedResponse<T>
            {
                Data = response,
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?)null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
                NextPage = response.Any() ? nextPage : null,
                PreviousPage = previousPage
            };
        }
    }
}