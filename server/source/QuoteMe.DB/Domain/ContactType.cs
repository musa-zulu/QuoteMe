using System;

namespace QuoteMe.DB.Domain
{
    public class ContactType : EntityBase
    {
        public Guid ContactTypeID { get; set; }
        public string Name { get; set; }
    }
}
