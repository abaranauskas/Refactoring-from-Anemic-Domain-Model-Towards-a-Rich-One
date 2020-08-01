using CSharpFunctionalExtensions;
using System;

namespace Logic.Customers
{
    public class CustomerStatus : ValueObject<CustomerStatus>
    {
        public static readonly CustomerStatus Regular =
            new CustomerStatus(ExpirationDate.Infinite, CustomerStatusType.Regular);

        private CustomerStatus(ExpirationDate expirationDate, CustomerStatusType customerStatusType)
        {
            _expirationDate = expirationDate ?? throw new ArgumentNullException(nameof(expirationDate));
            Type = customerStatusType;
        }

        private CustomerStatus()
        {
        }

        public bool IsAdvanced => Type == CustomerStatusType.Advanced && !ExpirationDate.IsExpired;

        private DateTime? _expirationDate;
        public ExpirationDate ExpirationDate => (ExpirationDate)_expirationDate;
        public CustomerStatusType Type { get; }

        public CustomerStatus Promote()
        {
            return new CustomerStatus((ExpirationDate)DateTime.UtcNow.AddYears(1), CustomerStatusType.Regular);
        }

        public decimal GetDiscount() => IsAdvanced ? 0.25m : 0m;

        protected override bool EqualsCore(CustomerStatus other)
        {
            return ExpirationDate == other.ExpirationDate && Type == other.Type;
        }

        protected override int GetHashCodeCore()
        {
            return ExpirationDate.GetHashCode() ^ Type.GetHashCode();
        }
    }

    public enum CustomerStatusType
    {
        Regular = 1,
        Advanced = 2
    }
}
