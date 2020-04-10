using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.DB;
using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuoteMe.Contracts.Services
{
    public class AddressService : IAddressService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public AddressService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public bool CreateAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            _applicationDbContext.Addresses.Add(address);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public bool DeleteAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            _applicationDbContext.Addresses.Remove(address);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public List<Address> GetAddress()
        {
            return _applicationDbContext
               ?.Addresses
               .ToList();
        }

        public Address GetAddressById(Guid addressID)
        {
            if (addressID == Guid.Empty)
                throw new ArgumentNullException(nameof(addressID));
            return _applicationDbContext.Addresses.FirstOrDefault(x => x.AddressID == addressID);
        }

        public bool UpdateAddress(Address addressToUpdate)
        {
            if (addressToUpdate == null)
                throw new ArgumentNullException(nameof(addressToUpdate));
            var address = GetAddressById(addressToUpdate.AddressID);

            if (address != null)
            {
                address.AddedBy = addressToUpdate.AddedBy;
                address.AddressID = addressToUpdate.AddressID;
                address.AddressLine1 = addressToUpdate.AddressLine1;
                address.AddressLine2 = addressToUpdate.AddressLine2;
                address.AddressTypeID = addressToUpdate.AddressTypeID;
                address.CityOrTown = addressToUpdate.CityOrTown;
                address.DateCreated = addressToUpdate.DateCreated;
                address.DateLastModified = addressToUpdate.DateLastModified;
                address.LastUpdatedBy = addressToUpdate.LastUpdatedBy;
                address.PostalCode = addressToUpdate.PostalCode;
                address.SpecialDescription = addressToUpdate.SpecialDescription;
                address.StateProvinceID = addressToUpdate.StateProvinceID;

            }
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}
