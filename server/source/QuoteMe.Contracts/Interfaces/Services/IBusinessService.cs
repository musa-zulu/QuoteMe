using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;

namespace QuoteMe.Contracts.Interfaces.Services
{
    public interface IBusinessService
    {
        List<Business> GetBusinesses();
        bool CreateBusiness(Business business);
        Business GetBusinessById(Guid businessID);
        bool UpdateBusiness(Business businessToUpdate);
        bool DeleteBusiness(Business business);
    }
}
