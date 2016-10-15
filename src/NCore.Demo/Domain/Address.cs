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
    }
}