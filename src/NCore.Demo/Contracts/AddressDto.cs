using NCore.Demo.Domain;

namespace NCore.Demo.Contracts
{
    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public static AddressDto FromAddress(Address address)
        {
            return new AddressDto
            {
                Country = address.Country,
                Street = address.Street,
                City = address.City
            };
        }
    }
}