using System;

namespace QuoteMe.DB.Domain
{
    public class ServicesOffered : EntityBase
    {
        public Guid ServicesOfferedID { get; set; }
        public string Description { get; set; }
        public Guid ServiceTypeID { get; set; }
    }
}
