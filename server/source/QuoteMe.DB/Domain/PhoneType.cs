using System;

namespace QuoteMe.DB.Domain
{
    public class PhoneType : EntityBase
    {
        public Guid PhoneTypeID { get; set; }
        public string Name { get; set; }
    }
}
