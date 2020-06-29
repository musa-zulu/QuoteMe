using System;

namespace QuoteMe.DB.Domain
{
    public class ServicesOffered : EntityBase
    {
        public Guid ServicesOfferedId { get; set; }
        public string Description { get; set; }
        public Guid ServiceTypeId { get; set; }
    }
}
