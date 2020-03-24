using System;

namespace QuoteMe.DB.Domain
{
    public class CountryRegion : EntityBase
    {
        public Guid CountryRegionID { get; set; }
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
    }
}
