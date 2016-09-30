namespace NCore.Demo.Domain
{
    public class Address : IValueType
    {
        protected Address()
        {
        }

        public Address(string street, string city, string country)
        {
            Street = street;
            City = city;
            Country = country;
        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
    }
}