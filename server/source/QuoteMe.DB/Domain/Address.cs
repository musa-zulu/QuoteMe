using System;

namespace QuoteMe.DB.Domain
{
    public class Address : EntityBase
    {
        public Guid AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CityOrTown { get; set; }
        public int PostalCode { get; set; }
        public string SpecialDescription { get; set; }
        public Guid StateProvinceId { get; set; }
        public Guid AddressTypeId { get; set; }
    }
}
