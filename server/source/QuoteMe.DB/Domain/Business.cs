using System;

namespace QuoteMe.DB.Domain
{
    public class Business : EntityBase
    {
        public Guid BusinessID { get; set; }
        public string Name { get; set; }
        public string Demographics { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public Guid PhoneID { get; set; }
        public Guid AddressID { get; set; }
        public Guid ServicesOfferedID { get; set; }
    }
}
