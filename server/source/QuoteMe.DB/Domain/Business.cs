using System;
using System.Collections.Generic;

namespace QuoteMe.DB.Domain
{
    public class Business : EntityBase
    {
        public Guid BusinessID { get; set; }
        public string Name { get; set; }
        public string Demographics { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public List<Phone> PhoneNumbers { get; set; }
        public List<Address> Addresses { get; set; }
        public List<ServicesOffered> ServicesOffered { get; set; }
    }
}
