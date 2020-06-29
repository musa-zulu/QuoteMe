using System;

namespace QuoteMe.DB.Domain
{
    public class ServiceType : EntityBase
    {
        public Guid ServiceTypeId { get; set; }
        public string Name { get; set; }
    }
}
