﻿using QuoteMe.Contracts.Interfaces.Services;
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

        public ServiceType GetServiceTypeById(Guid serviceTypeId)
        {
            if (serviceTypeId == Guid.Empty)
                throw new ArgumentNullException(nameof(serviceTypeId));
            return _applicationDbContext.ServiceTypes.FirstOrDefault(x => x.ServiceTypeId == serviceTypeId);
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
            _applicationDbContext.ServiceTypes.Update(serviceTypeToUpdate);
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}
