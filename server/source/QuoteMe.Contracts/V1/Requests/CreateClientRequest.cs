using System;

namespace QuoteMe.Contracts.V1.Requests
{
    public class CreateClientRequest
    {
        public Guid ClientId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Email { get; set; }
        public bool EmailPromotion { get; set; }
        public string AdditionalContactInfo { get; set; }
        public string Demographics { get; set; }
        public Guid PersonTypeId { get; set; }
        public Guid AddressId { get; set; }
        public Guid PhoneId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}