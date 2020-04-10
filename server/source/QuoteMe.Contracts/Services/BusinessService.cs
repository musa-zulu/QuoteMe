using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.DB;
using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuoteMe.Contracts.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public BusinessService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public bool CreateBusiness(Business business)
        {
            if (business == null)
                throw new ArgumentNullException(nameof(business));

            _applicationDbContext.Businesses.Add(business);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public bool DeleteBusiness(Business business)
        {
            if (business == null)
                throw new ArgumentNullException(nameof(business));

            _applicationDbContext.Businesses.Remove(business);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public Business GetBusinessById(Guid businessID)
        {
            if (businessID == Guid.Empty)
                throw new ArgumentNullException(nameof(businessID));
            return _applicationDbContext.Businesses.FirstOrDefault(x => x.BusinessID == businessID);
        }

        public List<Business> GetBusinesses()
        {
            return _applicationDbContext
               ?.Businesses
               .ToList();
        }

        public bool UpdateBusiness(Business businessToUpdate)
        {
            if (businessToUpdate == null)
                throw new ArgumentNullException(nameof(businessToUpdate));
            var business = GetBusinessById(businessToUpdate.BusinessID);

            if (business != null)
            {
                business.AddedBy = businessToUpdate.AddedBy;
                business.Addresses = businessToUpdate.Addresses;
                business.DateCreated = businessToUpdate.DateCreated;
                business.DateLastModified = businessToUpdate.DateLastModified;
                business.Demographics = businessToUpdate.Demographics;
                business.Description = businessToUpdate.Description;
                business.Email = businessToUpdate.Email;
                business.LastUpdatedBy = businessToUpdate.LastUpdatedBy;
                business.Name = businessToUpdate.Name;
                business.PhoneNumbers = businessToUpdate.PhoneNumbers;
                business.ServicesOffered = businessToUpdate.ServicesOffered;

            }
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}
