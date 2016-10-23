using NCore.Web.Api.Contracts;

namespace NCore.Demo.Contracts
{
    public class InvoiceDto : EntityDto,IHasCompanyDto
    {
        public long? CustomerId { get; set; }
        public string CustomerCompanyName { get; set; }
        public int DateDays { get; set; }
        public AddressDto Address { get; set; }
        public long CompanyId { get; set; }
    }
}