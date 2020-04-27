using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.DB;
using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuoteMe.Contracts.Services
{
    public class ServiceOfferedService : IServiceOfferedService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public ServiceOfferedService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }
        public bool CreateServiceOffered(ServicesOffered servicesOffered)
        {
            if (servicesOffered == null)
                throw new ArgumentNullException(nameof(servicesOffered));

            _applicationDbContext.ServicesOffered.Add(servicesOffered);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public bool DeleteServiceOffered(ServicesOffered servicesOffered)
        {
            if (servicesOffered == null)
                throw new ArgumentNullException(nameof(servicesOffered));

            _applicationDbContext.ServicesOffered.Remove(servicesOffered);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public ServicesOffered GetServiceOfferedById(Guid serviceOfferedID)
        {
            if (serviceOfferedID == Guid.Empty)
                throw new ArgumentNullException(nameof(serviceOfferedID));
            return _applicationDbContext.ServicesOffered.FirstOrDefault(x => x.ServicesOfferedID == serviceOfferedID);
        }

        public List<ServicesOffered> GetServicesOffered()
        {
            return _applicationDbContext
                ?.ServicesOffered
                .ToList();
        }

        public bool UpdateServiceOffered(ServicesOffered serviceOfferedToUpdate)
        {
            if (serviceOfferedToUpdate == null)
                throw new ArgumentNullException(nameof(serviceOfferedToUpdate));
            _applicationDbContext.ServicesOffered.Update(serviceOfferedToUpdate);
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}
