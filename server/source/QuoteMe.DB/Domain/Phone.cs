using System;

namespace QuoteMe.DB.Domain
{
    public class Phone : EntityBase
    {
        public Guid PhoneId { get; set; }
        public long PhoneNumber { get; set; }
        public Guid PhoneTypeId { get; set; }
    }
}
