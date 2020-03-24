using System;

namespace QuoteMe.DB.Domain
{
    public class StateOrProvince : EntityBase
    {
        public Guid StateOrProvinceID { get; set; }
        public string Name { get; set; }
        public string StateProvinceCode { get; set; }
        public Guid CountryRegionID { get; set; }
    }
}
