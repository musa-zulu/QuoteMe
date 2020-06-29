using System;

namespace QuoteMe.DB.Domain
{
    public class PhoneType : EntityBase
    {
        public Guid PhoneTypeId { get; set; }
        public string Name { get; set; }
    }
}
