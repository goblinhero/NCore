using NCore.Web.Api.Contracts;

namespace NCore.Demo.Contracts
{
    public class OrderDto : EntityDto,IHasCompanyDto
    {
        public long? CustomerId { get; set; }
        public string CustomerCompanyName { get; set; }
        public AddressDto Address { get; set; }
        public long CompanyId { get; set; }
    }
}