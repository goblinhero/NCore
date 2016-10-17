namespace NCore.Demo.Domain
{
    public class Address : IValueType
    {
        protected Address()
        {
        }

        public Address(string street, string city, string country)
        {
            Street = street ?? string.Empty;
            City = city ?? string.Empty;
            Country = country ?? string.Empty;
        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        public static Address Blank => new Address(string.Empty, string.Empty, string.Empty);

        protected bool Equals(Address other)
        {
            return string.Equals(Street, other.Street) && string.Equals(City, other.City) && string.Equals(Country, other.Country);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Address) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Street.GetHashCode();
                hashCode = (hashCode*397) ^ City.GetHashCode();
                hashCode = (hashCode*397) ^ Country.GetHashCode();
                return hashCode;
            }
        }
    }
}