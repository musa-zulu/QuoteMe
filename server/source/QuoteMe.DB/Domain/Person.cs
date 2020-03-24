using System;

namespace QuoteMe.DB.Domain
{
    public class Person : EntityBase
    {
        public Guid PersonID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Email { get; set; }
        public bool EmailPromotion { get; set; }
        public string AdditionalContactInfo { get; set; }
        public string Demographics { get; set; }
        public Guid PersonTypeID { get; set; }
        public Guid AddressID { get; set; }
        public Guid PhoneID { get; set; }
    }
}
