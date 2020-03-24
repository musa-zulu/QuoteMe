using System;

namespace QuoteMe.DB.Domain
{
    public class Phone : EntityBase
    {
        public Guid PhoneID { get; set; }
        public long PhoneNumber { get; set; }
        public Guid PhoneTypeID { get; set; }
    }
}
