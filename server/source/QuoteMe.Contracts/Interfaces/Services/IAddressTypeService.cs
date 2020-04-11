using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;

namespace QuoteMe.Contracts.Interfaces.Services
{
    public interface IAddressTypeService
    {
        List<AddressType> GetAddressTypes();
        bool CreateAddressType(AddressType addressType);
        AddressType GetAddressTypeById(Guid addressTypeID);
        bool UpdateAddressType(AddressType addressTypeToUpdate);
        bool DeleteAddressType(AddressType addressType);
    }
}
