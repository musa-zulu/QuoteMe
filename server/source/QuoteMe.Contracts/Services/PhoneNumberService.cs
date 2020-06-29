using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.DB;
using QuoteMe.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuoteMe.Contracts.Services
{
    public class PhoneNumberService : IPhoneNumberService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public PhoneNumberService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }
        public bool CreatePhoneNumber(Phone phone)
        {
            if (phone == null)
                throw new ArgumentNullException(nameof(phone));

            _applicationDbContext.PhoneNumbers.Add(phone);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public bool DeletePhoneNumber(Phone phone)
        {
            if (phone == null)
                throw new ArgumentNullException(nameof(phone));

            _applicationDbContext.PhoneNumbers.Remove(phone);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public Phone GetPhoneNumberById(Guid phoneId)
        {
            if (phoneId == Guid.Empty)
                throw new ArgumentNullException(nameof(phoneId));
            return _applicationDbContext.PhoneNumbers.FirstOrDefault(x => x.PhoneId == phoneId);
        }

        public List<Phone> GetPhoneNumbers()
        {
            return _applicationDbContext
               ?.PhoneNumbers
               .ToList();
        }

        public bool UpdatePhoneNumber(Phone phoneToUpdate)
        {
            if (phoneToUpdate == null)
                throw new ArgumentNullException(nameof(phoneToUpdate));
            _applicationDbContext.PhoneNumbers.Update(phoneToUpdate);
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}
