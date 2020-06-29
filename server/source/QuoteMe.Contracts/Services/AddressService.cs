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

        public Address GetAddressById(Guid addressId)
        {
            if (addressId == Guid.Empty)
                throw new ArgumentNullException(nameof(addressId));
            return _applicationDbContext.Addresses.FirstOrDefault(x => x.AddressId == addressId);
        }

        public bool UpdateAddress(Address addressToUpdate)
        {
            if (addressToUpdate == null)
                throw new ArgumentNullException(nameof(addressToUpdate));
            _applicationDbContext.Addresses.Update(addressToUpdate);
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}
