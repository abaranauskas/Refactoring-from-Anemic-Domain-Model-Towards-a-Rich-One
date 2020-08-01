using CSharpFunctionalExtensions;
using System;

namespace Logic.Customers
{
    public class ExpirationDate : ValueObject<ExpirationDate>
    {
        public static readonly ExpirationDate Infinite = new ExpirationDate(null);

        private ExpirationDate(DateTime? value)
        {
            Date = value;
        }

        public bool IsExpired => this != Infinite && Date < DateTime.UtcNow;

        public DateTime? Date { get; }

        public static Result<ExpirationDate> Create(DateTime date)
        {
            return new ExpirationDate(date);
        }

        protected override bool EqualsCore(ExpirationDate other)
        {
            return Date == other.Date;
        }

        protected override int GetHashCodeCore()
        {
            return Date.GetHashCode();
        }

        public static implicit operator DateTime?(ExpirationDate date)
        {
            return date.Date;
        }

        public static explicit operator ExpirationDate(DateTime? date)
        {
            if (date.HasValue)
                return Create(date.Value).Value;

            return Infinite;
        }
    }
}
