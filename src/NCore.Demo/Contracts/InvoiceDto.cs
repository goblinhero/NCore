using NCore.Web.Api.Contracts;

namespace NCore.Demo.Contracts
{
    public class InvoiceDto : EntityDto
    {
        public long? CustomerId { get; set; }
        public string CustomerCompanyName { get; set; }
        public int DateDays { get; set; }
        public AddressDto Address { get; set; }
    }
}