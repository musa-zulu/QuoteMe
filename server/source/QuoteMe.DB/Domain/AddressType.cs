using System;

namespace QuoteMe.DB.Domain
{
    public class AddressType : EntityBase
    {
        public Guid AddressTypeID { get; set; }
        public string Name { get; set; }
    }
}
