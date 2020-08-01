using CSharpFunctionalExtensions;
using System;

namespace Logic.Customers
{
    public class CustomerName : ValueObject<CustomerName>
    {
        private CustomerName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        //factory method
        public static Result<CustomerName> Create(string name)
        {
            name = (name ?? string.Empty).Trim();

            if (name.Length == 0)
                return Result.Failure<CustomerName>("Customer name should not be empty");


            if (name.Length > 100)
                return Result.Failure<CustomerName>("Customer name is too long");

            return Result.Success(new CustomerName(name));
        }

        protected override bool EqualsCore(CustomerName other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator string(CustomerName customerName)
        {
            return customerName.Value;
        }

        public static explicit operator CustomerName(string customerName)
        {
            return Create(customerName).Value;
        }
    }
}
