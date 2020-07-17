using System;

namespace QuoteMe.Contracts.Helpers
{
    public class DefaultDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
        public DateTime Today => DateTime.Today;
    }
}