using System;

namespace QuoteMe.DB.Domain
{
    public class EntityBase
    {
        public string AddedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public Guid rowguid { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}
