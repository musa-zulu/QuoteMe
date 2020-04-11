using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;

namespace QuoteMe.Contracts.Interfaces.Services
{
    public interface IPhoneNumberService
    {
        List<Phone> GetPhoneNumbers();
        bool CreatePhoneNumber(Phone phone);
        Phone GetPhoneNumberById(Guid phoneID);
        bool UpdatePhoneNumber(Phone phoneToUpdate);
        bool DeletePhoneNumber(Phone phone);
    }
}
