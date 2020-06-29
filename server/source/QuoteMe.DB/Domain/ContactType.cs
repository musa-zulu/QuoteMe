using System;

namespace QuoteMe.DB.Domain
{
    public class ContactType : EntityBase
    {
        public Guid ContactTypeId { get; set; }
        public string Name { get; set; }
    }
}
