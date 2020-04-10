using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;

namespace QuoteMe.Contracts.Interfaces.Services
{
    public interface IAddressService
    {
        List<Address> GetAddress();
        bool CreateAddress(Address address);
        Address GetAddressById(Guid addressID);
        bool UpdateAddress(Address addressToUpdate);
        bool DeleteAddress(Address address);
    }
}
