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

        public Business GetBusinessById(Guid businessId)
        {
            if (businessId == Guid.Empty)
                throw new ArgumentNullException(nameof(businessId));
            return _applicationDbContext.Businesses.FirstOrDefault(x => x.BusinessId == businessId);
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
            _applicationDbContext.Businesses.Update(businessToUpdate);
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}
