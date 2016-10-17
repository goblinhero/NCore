using NCore.Web.Api.Contracts;

namespace NCore.Demo.Contracts
{
    public class CustomerDto : EntityDto
    {
        public string CompanyName { get; set; }
        public AddressDto Address { get; set; }
    }
}