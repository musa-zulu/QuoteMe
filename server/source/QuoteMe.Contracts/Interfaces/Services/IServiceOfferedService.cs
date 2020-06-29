using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;

namespace QuoteMe.Contracts.Interfaces.Services
{
    public interface IServiceOfferedService
    {
        List<ServicesOffered> GetServicesOffered();
        bool CreateServiceOffered(ServicesOffered servicesOffered);
        ServicesOffered GetServiceOfferedById(Guid serviceOfferedId);
        bool UpdateServiceOffered(ServicesOffered serviceOfferedToUpdate);
        bool DeleteServiceOffered(ServicesOffered servicesOffered);
    }
}
