using System;

namespace QuoteMe.DB.Domain
{
    public class ServiceType : EntityBase
    {
        public Guid ServiceTypeID { get; set; }
        public string Name { get; set; }
    }
}
