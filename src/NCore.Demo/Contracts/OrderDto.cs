using NCore.Nancy.Api.Contracts;

namespace NCore.Demo.Contracts
{
    public class OrderDto : EntityDto
    {
        public long? CustomerId { get; set; }
        public string CustomerCompanyName { get; set; }
        public AddressDto Address { get; set; }
    }
}