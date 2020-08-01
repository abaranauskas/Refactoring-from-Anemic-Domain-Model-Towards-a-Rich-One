using CSharpFunctionalExtensions;
using System;
using System.Text.RegularExpressions;

namespace Logic.Customers
{
    public class Email : ValueObject<Email>
    {
        private Email(string value)
        {
            Value = value;
        }
        public string Value { get; }

        public static Result<Email> Create(string email)
        {
            email = (email ?? string.Empty).Trim();

            if (email.Length == 0)
                return Result.Failure<Email>("Customer name should not be empty");


            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                return Result.Failure<Email>("Email is invalid");

            return Result.Success(new Email(email));
        }

        protected override bool EqualsCore(Email other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }

        public static explicit operator Email(string email)
        {
            return Create(email).Value;
        }
    }
}
