using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;

namespace QuoteMe.Contracts.Interfaces.Services
{
    public interface IServiceTypesOfferedService
    {
        List<ServiceType> GetServiceTypes();
        bool CreateServiceType(ServiceType serviceType);
        ServiceType GetServiceTypeById(Guid serviceTypeID);
        bool UpdateServiceType(ServiceType serviceTypeToUpdate);
        bool DeleteServiceType(ServiceType serviceType);
    }
}
