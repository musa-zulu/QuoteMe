using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.DB;
using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuoteMe.Contracts.Services
{
    public class AddressTypeService : IAddressTypeService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public AddressTypeService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }
        public bool CreateAddressType(AddressType addressType)
        {
            if (addressType == null)
                throw new ArgumentNullException(nameof(addressType));

            _applicationDbContext.AddressTypes.Add(addressType);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public bool DeleteAddressType(AddressType addressType)
        {
            if (addressType == null)
                throw new ArgumentNullException(nameof(addressType));

            _applicationDbContext.AddressTypes.Remove(addressType);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public AddressType GetAddressTypeById(Guid addressTypeID)
        {
            if (addressTypeID == Guid.Empty)
                throw new ArgumentNullException(nameof(addressTypeID));
            return _applicationDbContext.AddressTypes.FirstOrDefault(x => x.AddressTypeID == addressTypeID);
        }

        public List<AddressType> GetAddressTypes()
        {
            return _applicationDbContext
               ?.AddressTypes
               .ToList();
        }

        public bool UpdateAddressType(AddressType addressTypeToUpdate)
        {
            if (addressTypeToUpdate == null)
                throw new ArgumentNullException(nameof(addressTypeToUpdate));
            var addressType = GetAddressTypeById(addressTypeToUpdate.AddressTypeID);

            if (addressType != null)
            {
                addressType.AddedBy = addressTypeToUpdate.AddedBy;
                addressType.DateCreated = addressTypeToUpdate.DateCreated;
                addressType.DateLastModified = addressTypeToUpdate.DateLastModified;
                addressType.LastUpdatedBy = addressTypeToUpdate.LastUpdatedBy;
                addressType.Name = addressTypeToUpdate.Name;
            }
            return _applicationDbContext.SaveChanges() > 0;
        }

    }
}
