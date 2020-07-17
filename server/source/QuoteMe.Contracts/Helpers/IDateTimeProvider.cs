using System;

namespace QuoteMe.Contracts.Helpers
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        DateTime Today { get; }
    }
}