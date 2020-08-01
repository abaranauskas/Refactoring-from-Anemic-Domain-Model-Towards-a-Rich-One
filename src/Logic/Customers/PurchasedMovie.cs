using Logic.Common;
using Logic.Movies;
using System;

namespace Logic.Customers
{
    public class PurchasedMovie : Entity
    {
        protected PurchasedMovie()
        {
        }

        internal PurchasedMovie(Movie movie, Customer customer, Dollars price, ExpirationDate expirationDate)
        {
            if (price is null || price.IsZero)
                throw new ArgumentException(nameof(price));

            if (expirationDate is null || expirationDate.IsExpired)
                throw new ArgumentException(nameof(expirationDate));

            Movie = movie ?? throw new ArgumentNullException(nameof(movie));
            Customer = customer ?? throw new ArgumentNullException(nameof(customer)); ;
            Price = price;
            ExpirationDate = expirationDate;
            PurchaseDate = DateTime.UtcNow;
        }

        public virtual Movie Movie { get; protected set; }
        public virtual Customer Customer { get; protected set; }

        private Dollars _price;
        public virtual Dollars Price
        {
            get => Dollars.Of(_price);
            protected set => _price = value;
        }

        public virtual DateTime PurchaseDate { get; protected set; }


        private DateTime? _expirationDate;

        public virtual ExpirationDate ExpirationDate
        {
            get => (ExpirationDate)_expirationDate;
            protected set => _expirationDate = value;
        }
    }
}
