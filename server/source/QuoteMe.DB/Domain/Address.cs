using System;

namespace QuoteMe.DB.Domain
{
    public class Address : EntityBase
    {
        public Guid AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CityOrTown { get; set; }
        public int PostalCode { get; set; }
        public string SpecialDescription { get; set; }
        public Guid StateProvinceID { get; set; }
        public Guid AddressTypeID { get; set; }
    }
}
