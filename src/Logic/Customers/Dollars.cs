using CSharpFunctionalExtensions;

namespace Logic.Customers
{
    public class Dollars : ValueObject<Dollars>
    {
        private const decimal MaxDollarAmount = 1000000;

        private Dollars(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public bool IsZero => Value == 0;

        public static Result<Dollars> Create(decimal dollarAmount)
        {
            if (dollarAmount % 0.01m > 0)
                return Result.Failure<Dollars>("Dollar amount cant not contain part of penny");

            if (dollarAmount < 0)
                return Result.Failure<Dollars>("Dollar amount cant not be negative");

            if (dollarAmount > MaxDollarAmount)
                return Result.Failure<Dollars>("Dollar amount cant not graater than " + MaxDollarAmount);

            return Result.Success(new Dollars(dollarAmount));
        }

        public static Dollars Of(decimal amount)
        {
            return Create(amount).Value;
        }

        protected override bool EqualsCore(Dollars other)
        {
            return Value.Equals(other.Value);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Dollars dollars)
        {
            return dollars.Value;
        }

        public static Dollars operator *(Dollars dollars, decimal multiplier)
        {
            return new Dollars(dollars.Value * multiplier);
        }

        public static Dollars operator +(Dollars left, Dollars right)
        {
            return new Dollars(left.Value + right.Value);
        }

        //public static explicit operator Dollars(decimal dollarAmount)
        //{
        //    return Create(dollarAmount).Value;
        //}
    }
}
