using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.DB;
using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuoteMe.Contracts.Services
{
    public class ServiceTypesOfferedService : IServiceTypesOfferedService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public ServiceTypesOfferedService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public bool CreateServiceType(ServiceType serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            _applicationDbContext.ServiceTypes.Add(serviceType);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public bool DeleteServiceType(ServiceType serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            _applicationDbContext.ServiceTypes.Remove(serviceType);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public ServiceType GetServiceTypeById(Guid serviceTypeID)
        {
            if (serviceTypeID == Guid.Empty)
                throw new ArgumentNullException(nameof(serviceTypeID));
            return _applicationDbContext.ServiceTypes.FirstOrDefault(x => x.ServiceTypeID == serviceTypeID);
        }

        public List<ServiceType> GetServiceTypes()
        {
            return _applicationDbContext
               ?.ServiceTypes
               .ToList();
        }

        public bool UpdateServiceType(ServiceType serviceTypeToUpdate)
        {
            if (serviceTypeToUpdate == null)
                throw new ArgumentNullException(nameof(serviceTypeToUpdate));
            var serviceType = GetServiceTypeById(serviceTypeToUpdate.ServiceTypeID);

            if (serviceType != null)
            {
                serviceType.AddedBy = serviceTypeToUpdate.AddedBy;
                serviceType.DateCreated = serviceTypeToUpdate.DateCreated;
                serviceType.DateLastModified = serviceTypeToUpdate.DateLastModified;
                serviceType.LastUpdatedBy = serviceTypeToUpdate.LastUpdatedBy;
                serviceType.Name = serviceTypeToUpdate.Name;
            }
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}
