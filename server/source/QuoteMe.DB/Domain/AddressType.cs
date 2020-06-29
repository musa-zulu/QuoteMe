using System;

namespace QuoteMe.DB.Domain
{
    public class AddressType : EntityBase
    {
        public Guid AddressTypeId { get; set; }
        public string Name { get; set; }
    }
}
