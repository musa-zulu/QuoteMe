using System;

namespace QuoteMe.DB.Domain
{
    public class StateOrProvince : EntityBase
    {
        public Guid StateOrProvinceId { get; set; }
        public string Name { get; set; }
        public string StateProvinceCode { get; set; }
        public Guid CountryRegionId { get; set; }
    }
}
